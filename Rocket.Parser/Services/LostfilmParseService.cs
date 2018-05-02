using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using AngleSharp.Dom;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.Parser.Exceptions;
using Rocket.Parser.Heplers;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;

namespace Rocket.Parser.Services
{
    /// <summary>
    /// Парсер Lostfilm
    /// </summary>
    internal class LostfilmParseService : ILostfilmParseService
    {
        private readonly ILoadHtmlService _loadHtmlService;

        public LostfilmParseService(ILoadHtmlService loadHtmlService)
        {
            _loadHtmlService = loadHtmlService;
        }

        public void Parse()
        {
            try
            {
                //Получаем базовые настройки для загрузки сериалов
                var baseUrl = LostfilmHelper.GetBaseUrl(); //todo эта настройка должна лежать в базе в админке
                string messageNotFoundByRequest = LostfilmHelper.GetMessageNotFoundByRequest();
                int.TryParse(LostfilmHelper.GetTakeTvSerialByRequest(), out int takeTvSerialByRequest);
                
                IElement elSerialList;
                var listLostfilmSerialModel = new List<LostfilmSerialModel>();
                int getSerialListIteration = 0;
                do
                {
                    //Получаем список сериалов пачками по 10 штук(больше сайт не отдает за раз) с проверкой доступности сайта.
                    elSerialList = GetStartElementForParse(baseUrl, ref getSerialListIteration, takeTvSerialByRequest);

                    //Перебираем сериалы пока не закончились в подгруженном списке
                    int i = 2;
                    do
                    {
                        var lostfilmSerialModel = new LostfilmSerialModel();

                        //Парсим заголовок сериала из списка сериалов
                        if (!ParseSerialHeader(baseUrl, elSerialList, lostfilmSerialModel, i)) break;

                        //Парсим обзорную информацию по сериалу
                        ParseTvSerialOverviewDetails(lostfilmSerialModel);

                        //Парсим все серии и сериала.
                        ParseOverviewTvSerial(lostfilmSerialModel);

                        listLostfilmSerialModel.Add(lostfilmSerialModel);

                        i = i + 2;
                        if (i > 1000) break; //для подстраховки
                    } while (true);
                    

                } while (elSerialList.InnerHtml.IndexOf(messageNotFoundByRequest, StringComparison.Ordinal) < 0);
                
                //todo сдулать парсинг персон!
                //todo сделать запихивание данных в бд

                var t = 1211;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Получаем список сериалов пачками по 10 штук(больше сайт не отдает за раз) с проверкой доступности сайта.
        /// </summary>
        /// <param name="baseUrl">Начальная ссылка.</param>
        /// <param name="getSerialListIteration">Итерация получения списка сериалов.</param>
        /// <param name="takeTvSerialByRequest">Кол-во сериалов выдаваемых сайтом за раз.</param>
        /// <returns>Элемент со списком сериалов.</returns>
        private IElement GetStartElementForParse(string baseUrl, ref int getSerialListIteration, int takeTvSerialByRequest)
        {

            int.TryParse(LostfilmHelper.GetRetryCountIfNotResponse(), out int retryCountIfNotResponse);
            int.TryParse(LostfilmHelper.GetWaitBeforRetryInSeconds(), out int waitBeforRetryInSeconds);

            IElement elSerialList = null;
            int currentRetryCount = 0;
            do
            {
                try
                {
                    //Получаем элемент со списком сериалов
                    var htmlDocumentSerialList = _loadHtmlService.GetHtmlDocumentByUrlAsync(
                        baseUrl + LostfilmHelper.GetAdditionalUrlToSerialList() +
                        $"?act=serial&type=search&o={getSerialListIteration}&s=3&t=0");
                    elSerialList = htmlDocumentSerialList.QuerySelector(LostfilmHelper.GetTvSerailListHeaderBase());
                }
                catch (NotGetHtmlDocumentByUrlException)
                {
                    //todo запись в лог о неудачной попытке обратиться к сайту
                    if (currentRetryCount > retryCountIfNotResponse) throw;
                    Thread.Sleep(waitBeforRetryInSeconds * 1000);
                }

                getSerialListIteration = getSerialListIteration + takeTvSerialByRequest;
                currentRetryCount++;
            } while (elSerialList == null);
            
            return elSerialList;
        }

        /// <summary>
        /// Парсим заголовок сериала из списка сериалов.
        /// </summary>
        /// <param name="baseUrl">Начальная ссылка.</param>
        /// <param name="elSerialList">Элемент список заголовков сериалов.</param>
        /// <param name="lostfilmSerialModel">Модель для временной агрегации данных результата парсинга.</param>
        /// <param name="i">Счетчик.</param>
        /// <returns>true - если сериал успешно получен, false - не удалось получить следующий сериал.</returns>
        private bool ParseSerialHeader(string baseUrl, IElement elSerialList, LostfilmSerialModel lostfilmSerialModel, int i)
        {
            var serialTopElement = elSerialList.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeader(), i));
            if (serialTopElement == null) return false;

            var tvSeriasEntity = new TvSeriasEntity
            {
                ListSeasons = new List<SeasonEntity>(),
                ListGenreEntity = new List<GenreEntity>(),
                ListActor = new List<PersonEntity>(),
                ListDirector = new List<PersonEntity>(),
                ListProducer = new List<PersonEntity>(),
                ListWriter = new List<PersonEntity>()
            };

            //Получаем элемент детализации по сериалу из заголовка
            var addUrlForDetailElement = serialTopElement
                .QuerySelector(string.Format(LostfilmHelper.GetTvSerialHeaderDetail(), i));

            //Получаем дополнительную ссылку для получения деталей по сериалу.
            tvSeriasEntity.UrlToSource = baseUrl + addUrlForDetailElement.GetAttribute(CommonHelper.HrefAttribute);

            //todo сделать проверку на наличие сериала в бд по ссылке, если есть код ниже выполняться не должен
            //todo кроме обновление рейтинга и текущего статуса сериала!!!

            //Получаем ссылку на изображение-миниатюру для сериала.
            var imageUrlTvSerialThumbElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderDetailImageUrlThumb(), i));
            tvSeriasEntity.PosterImageUrl =
                CommonHelper.HttpText + imageUrlTvSerialThumbElement.GetAttribute(CommonHelper.SrcAttribute);

            //Получаем название сериала по-русски.
            var tvSerialNameRuElement = serialTopElement.QuerySelector(
                    string.Format(LostfilmHelper.GetTvSerialHeaderDetailTvSerialNameRu(), i));
            tvSeriasEntity.TitleRu = tvSerialNameRuElement?.InnerHtml;

            //Получаем название сериала по-английски.
            var tvSerialNameEnElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderDetailTvSerialNameEn(), i));
            tvSeriasEntity.TitleEn = tvSerialNameEnElement?.InnerHtml;

            //Получаем рейтинг сериала на Lostfilm.
            var lostfilmRateElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderLostfilmRate(), i));
            double.TryParse(lostfilmRateElement.InnerHtml, out double lostfilmRate);
            tvSeriasEntity.LostfilmRate = lostfilmRate;

            lostfilmSerialModel.TvSeriasEntity = tvSeriasEntity;

            //Парсим заголовок сериала из списка сериалов панель детализации
            ParseSerialHeaderDetailsPane(serialTopElement, lostfilmSerialModel, i);

            return true;
        }

        /// <summary>
        /// Парсим заголовок сериала из списка сериалов панель детализации.
        /// </summary>
        /// <param name="serialTopElement">Элемент заголовка сериала.</param>
        /// <param name="lostfilmSerialModel">Модель для временной агрегации данных результата парсинга.</param>
        /// <param name="i">Счетчик елементов заголовка сериала.</param>
        private void ParseSerialHeaderDetailsPane(IElement serialTopElement,
            LostfilmSerialModel lostfilmSerialModel, int i)
        {
            //Получаем панель деталей
            var detailsPaneElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderDetailPane(), i));
            string detailsPane = detailsPaneElement.InnerHtml;

            //Получаем текущий статус сериала
            lostfilmSerialModel.TvSeriasEntity.CurrentStatus =
                StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordStatus(),
                    CommonHelper.OpenAngleBracket);

            //Получаем теливизионный канал на котором показывают сериал
            lostfilmSerialModel.TvSeriasEntity.TvSerialCanal =
                StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordCanal(),
                    CommonHelper.OpenAngleBracket);

            //Получаем список жанров в виде строки для последующего парсинга.
            lostfilmSerialModel.ListGenreForParse =
                StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordGenre(),
                    CommonHelper.OpenAngleBracket);

            //Получаем год начала показа сериала.
            lostfilmSerialModel.TvSeriasEntity.TvSerialYearStart =
                StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordYearStart(),
                    CommonHelper.OpenAngleBracket);
        }

        /// <summary>
        /// Парсим обзорную информацию по сериалу
        /// </summary>
        /// <param name="lostfilmSerialModel">Модель для временной агрегации данных результата парсинга.</param>
        private void ParseTvSerialOverviewDetails(LostfilmSerialModel lostfilmSerialModel)
        {
            var htmlDocumentSerialOverviewDetails =
                _loadHtmlService.GetHtmlDocumentByUrlAsync(lostfilmSerialModel.TvSeriasEntity.UrlToSource);

            var serialOverviewElement =
                htmlDocumentSerialOverviewDetails.QuerySelector(LostfilmHelper.GetTvSerialOverview());

            var serialOverviewText = serialOverviewElement.InnerHtml;

            //Получаем рейтинг ImDb
            string rateImDbText = StringHelper.GetSubstring(serialOverviewText,
                LostfilmHelper.GetTvSerialKeywordRateImDb(), CommonHelper.OpenAngleBracket);

            double.TryParse(rateImDbText, out double rateImDb);
            lostfilmSerialModel.TvSeriasEntity.RateImDb = rateImDb;

            //Получаем дату премьеры сериала прописью
            var premiereDateForParseElement = serialOverviewElement.QuerySelector(
                LostfilmHelper.GetTvSerialPremiereDateForParse());
            lostfilmSerialModel.PremiereDateForParse = premiereDateForParseElement?.InnerHtml; //todo распарсить

            //Получаем ссылку на ориганильный сайт
            var urlToOfficialSiteElement =
                serialOverviewElement.QuerySelector(LostfilmHelper.GetTvSerialUrlToOfficialSite());
            lostfilmSerialModel.TvSeriasEntity.UrlToOfficialSite = urlToOfficialSiteElement?.InnerHtml;

            //Полчаем описание сериала
            var summaryElement = serialOverviewElement.QuerySelector(LostfilmHelper.GetTvSerialSummary());
            lostfilmSerialModel.TvSeriasEntity.Summary = summaryElement?.InnerHtml;
            //todo доработать чтобы тут было только описание

            //todo страна

        }

        /// <summary>
        /// Парсим все серии и сериала.
        /// </summary>
        /// <param name="lostfilmSerialModel"> Модель для временной агрегации данных результата парсинга 
        /// (нужна чтобы потом сделать дополнительный парсинг и вставку в бд).</param>
        private void ParseOverviewTvSerial(LostfilmSerialModel lostfilmSerialModel)
        {
            try
            {
                var htmlDocumentTvSerialAllEpisodes = _loadHtmlService.GetHtmlDocumentByUrlAsync(
                                lostfilmSerialModel.TvSeriasEntity.UrlToSource + LostfilmHelper.AdditionalUrlToTvSerialEpisodes());

                var tvSerialAllEpisodesElement =
                    htmlDocumentTvSerialAllEpisodes.QuerySelector(LostfilmHelper.GetAllEpisodes());

                //Получает массив постеров сезонов.
                var arrSeasonPosters = GetSeasonPosters(tvSerialAllEpisodesElement);

                //Получаем список всех эпизодов
                var tvSerialAllEpisodesTableElement =
                    tvSerialAllEpisodesElement.GetElementsByTagName(CommonHelper.TbodyAttribute).First();
                var listTvSerialAllEpisodes =
                    tvSerialAllEpisodesTableElement.GetElementsByTagName(CommonHelper.TrAttribute);

                foreach (var episodeElement in listTvSerialAllEpisodes)
                {
                    //Получаем номер сериала и номер сезона.
                    GetSerialNumberAndEpisodeNumber(episodeElement, out int seasonsNumber, out int episodeNumber);

                    //Получаем сущность сезона.
                    var seasonEntity = GetSeasonEntity(lostfilmSerialModel, seasonsNumber, arrSeasonPosters);

                    //Получаем сущность серии
                    var episodeEntity = GetEpisodeEntity(lostfilmSerialModel, seasonsNumber, episodeNumber);

                    //Получаем ссылку на серию.
                    GetUrlForEpisodeSource(episodeElement, episodeEntity);

                    //Получаем название серии (русское и английское).
                    GetEpisodeName(episodeElement, episodeEntity);

                    //Получаем даты релизов (русская версия и английская).
                    GetReleaseDate(episodeElement, episodeEntity);

                    seasonEntity.ListEpisode.Add(episodeEntity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        /// <summary>
        /// Получает массив постеров сезонов.
        /// </summary>
        /// <param name="tvSerialAllEpisodesElement">Элемент содержащий все серии.</param>
        /// <returns>Массив постеров сезонов.</returns>
        private string[] GetSeasonPosters(IElement tvSerialAllEpisodesElement)
        {
            var seasonPosters = tvSerialAllEpisodesElement.GetElementsByClassName(LostfilmHelper.GetSeasonPosters());
            int countSeasons = seasonPosters.Length;
            string[] arrSeasonPosters = new string[seasonPosters.Length];
            int i = 1;
            foreach (var seasonPoster in seasonPosters)
            {
                arrSeasonPosters[countSeasons - i] = 
                    CommonHelper.HttpText + StringHelper.GetSubstring(
                        seasonPoster.OuterHtml, CommonHelper.Apostrophe, CommonHelper.Apostrophe);
                i++;
            }

            return arrSeasonPosters;
        }

        /// <summary>
        /// Получаем сущность сезона.
        /// </summary>
        /// <param name="lostfilmSerialModel"> Модель для временной агрегации данных результата парсинга 
        /// (нужна чтобы потом сделать дополнительный парсинг и вставку в бд).</param>
        /// <param name="seasonsNumber">Номер сезона</param>
        /// <param name="arrSeasonPosters">массив постеров сезонов.</param>
        /// <returns>сущность сезона.</returns>
        private SeasonEntity GetSeasonEntity(LostfilmSerialModel lostfilmSerialModel, int seasonsNumber, string[] arrSeasonPosters)
        {
            var seasonEntity = lostfilmSerialModel.TvSeriasEntity.ListSeasons
                .FirstOrDefault(item => item.Number == seasonsNumber);
            if (seasonEntity == null)
            {
                seasonEntity = new SeasonEntity
                {
                    ListEpisode = new List<EpisodeEntity>(),
                    Number = seasonsNumber
                };

                if (arrSeasonPosters.Length >= seasonsNumber)
                {
                    seasonEntity.PosterImageUrl = arrSeasonPosters[seasonsNumber - 1];
                }

                lostfilmSerialModel.TvSeriasEntity.ListSeasons.Add(seasonEntity);
            }

            return seasonEntity;
        }

        /// <summary>
        /// Получаем номер сериала и номер сезона.
        /// </summary>
        /// <param name="episodeElement">Элемент серии.</param>
        /// <param name="seasonsNumber">Номер сезона(возвращаемое).</param>
        /// <param name="episodeNumber">Номер сериала(возвращаемое).</param>
        private void GetSerialNumberAndEpisodeNumber(IElement episodeElement, out int seasonsNumber, out int episodeNumber)
        {
            var episodeAndSeriaNumberForParse = episodeElement
                .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameBeta()).First().InnerHtml;

            var seasonNumberText = StringHelper.GetSubstring(
                episodeAndSeriaNumberForParse, string.Empty, LostfilmHelper.GetSeasonKeyword());
            var episodeNumberText = StringHelper.GetSubstring(episodeAndSeriaNumberForParse,
                LostfilmHelper.GetSeasonKeyword(), LostfilmHelper.GetEpisodeKeyword());

            int.TryParse(seasonNumberText, out int seasonsNumberRes);
            int.TryParse(episodeNumberText, out int episodeNumberRes);

            seasonsNumber = seasonsNumberRes;
            episodeNumber = episodeNumberRes;
        }

        /// <summary>
        /// Получаем сущность серии.
        /// </summary>
        /// <param name="lostfilmSerialModel"> Модель для временной агрегации данных результата парсинга 
        /// (нужна чтобы потом сделать дополнительный парсинг и вставку в бд).</param>
        /// <param name="seasonsNumber">Номер сезона</param>
        /// <param name="episodeNumber">Номер серии.</param>
        /// <returns>сущность серии.</returns>
        private EpisodeEntity GetEpisodeEntity(LostfilmSerialModel lostfilmSerialModel, int seasonsNumber, int episodeNumber)
        {
            var episodeEntity = lostfilmSerialModel.TvSeriasEntity.ListSeasons
                .First(item => item.Number == seasonsNumber)
                .ListEpisode
                .FirstOrDefault(item => item.Number == episodeNumber);

            if (episodeEntity != null) return episodeEntity;

            episodeEntity = new EpisodeEntity
            {
                Number = episodeNumber
            };

            return episodeEntity;
        }

        /// <summary>
        /// Получаем ссылку на серию.
        /// </summary>
        /// <param name="episodeElement">Элемент серии.</param>
        /// <param name="episodeEntity">Сущность эпизода.</param>
        private void GetUrlForEpisodeSource(IElement episodeElement, EpisodeEntity episodeEntity)
        {
            string outerHtmlBetaClass = episodeElement
                .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameBeta()).First().OuterHtml;
            episodeEntity.UrlForEpisodeSource =
                LostfilmHelper.GetBaseUrl() + StringHelper.GetSubstring(
                    outerHtmlBetaClass, CommonHelper.Apostrophe, CommonHelper.Apostrophe);
        }

        /// <summary>
        /// Получаем название серии (русское и английское).
        /// </summary>
        /// <param name="episodeElement">Элемент серии.</param>
        /// <param name="episodeEntity">Сущность эпизода.</param>
        private void GetEpisodeName(IElement episodeElement, EpisodeEntity episodeEntity)
        {
            var gammaClassElement = episodeElement
                .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameGamma()).First();
            string someTextForParseName = gammaClassElement.GetElementsByTagName(CommonHelper.DivAttribute)
                .FirstOrDefault()?.InnerHtml;
            if (!string.IsNullOrEmpty(someTextForParseName))
            {
                episodeEntity.TitleRu = StringHelper.GetSubstring(someTextForParseName,
                    string.Empty, CommonHelper.OpenAngleBracket, false);
                episodeEntity.TitleEn = episodeElement
                    .GetElementsByClassName(LostfilmHelper.GetGrayColor2SmallTextClass())
                    .First()
                    .InnerHtml;
            }
            else
            {
                someTextForParseName = gammaClassElement.OuterHtml;
                episodeEntity.TitleRu = StringHelper.GetSubstring(someTextForParseName,
                    CommonHelper.CloseAngleBracket, CommonHelper.BrAttribute + CommonHelper.CloseAngleBracket, false);
                episodeEntity.TitleEn = episodeElement
                    .GetElementsByClassName(LostfilmHelper.GetSmallTextClass())
                    .First()
                    .InnerHtml;
            }
        }

        /// <summary>
        /// Получаем даты релизов (русская версия и английская).
        /// </summary>
        /// <param name="episodeElement">Элемент серии.</param>
        /// <param name="episodeEntity">Сущность эпизода.</param>
        private void GetReleaseDate(IElement episodeElement, EpisodeEntity episodeEntity)
        {
            string someTextForParseDates = episodeElement
                .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameDelta())
                .First()
                .OuterHtml;

            string releaseDateRuText = StringHelper.GetSubstring(
                someTextForParseDates, LostfilmHelper.GetRuKeyword(), CommonHelper.OpenAngleBracket);
            DateTime.TryParseExact(releaseDateRuText, LostfilmHelper.GetDateFormat(),
                new CultureInfo(CommonHelper.CultureInfoText), DateTimeStyles.None, out DateTime releaseDateRu);
            episodeEntity.ReleaseDateRu = releaseDateRu;

            string releaseDateEnText = StringHelper.GetSubstring(
                someTextForParseDates, LostfilmHelper.GetEngKeyword(), CommonHelper.OpenAngleBracket);
            DateTime.TryParseExact(releaseDateEnText, LostfilmHelper.GetDateFormat(),
                new CultureInfo(CommonHelper.CultureInfoText), DateTimeStyles.None, out DateTime releaseDateEn);
            episodeEntity.ReleaseDateEn = releaseDateEn;
        }
    }
}
