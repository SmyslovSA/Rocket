using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Rocket.Parser.Heplers;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Parsers
{
    /// <summary>
    /// Парсер Lostfilm
    /// </summary>
    internal class LostfilmParser : ILostfilmParser
    {
        private readonly ILoadHtmlService _loadHtmlService;

        private readonly string _baseUrl;
        private readonly string _messageNotFoundByRequest;
        private readonly int _takeTvSeriasByRequest;

        public LostfilmParser(ILoadHtmlService loadHtmlService)
        {
            _loadHtmlService = loadHtmlService;

            //Получаем базовую ссылку
            _baseUrl = LostfilmHelper.GetBaseUrl(); //todo эта настройка должна лежать в базе в админке
            //Получаем текст сообщения достижения конца списка сериалов на сайте
            _messageNotFoundByRequest = LostfilmHelper.GetMessageNotFoundByRequest();
            //Получаем кол-во сериалов которые сайт выдает за раз
            int.TryParse(LostfilmHelper.GetTakeTvSeriasByRequest(), out _takeTvSeriasByRequest);
        }

        /// <summary>
        /// Парсим сайт Lostfilm
        /// </summary>
        public async Task ParseAsync()
        {
            try
            {
                var dtStart = DateTime.Now;

                //Получаем полный список элементов "список сериалов".
                var listTvSeriasListElement = GetListTvSeriasListElementAll();
                
                
                    //var listTvSeriasEntity = new ConcurrentBag<TvSeriasEntity>();

                    //Parallel.ForEach(
                    //    listTvSeriasListElement, (serialListElement) =>
                    //        ParseSerialListElement(serialListElement, listTvSeriasEntity));

                    //todo сделать запихивание данных в бд

                
                
                var duration = DateTime.Now - dtStart;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Получаем полный список элементов "список сериалов".
        /// </summary>
        /// <returns>Cписок элементов "список сериалов".</returns>
        private List<IElement> GetListTvSeriasListElementAll()
        {
            var listTvSeriasListElementAll = new List<IElement>();

            List<IElement> listTvSeriasListElementBatch;
            int getTvSeriasListIteration = 0;
            do
            {
                //Получаем частичный список элементов "список сериалов" с сайта.
                listTvSeriasListElementBatch = GetListTvSeriasListElementBatch(ref getTvSeriasListIteration);

                //Добавляем в общий список
                listTvSeriasListElementAll.AddRange(listTvSeriasListElementBatch);

                //Выполняем до тех пор пока сайт возвращает нам элементы "список сериалов"
            } while (listTvSeriasListElementBatch.Any());

            return listTvSeriasListElementAll;
        }

        /// <summary>
        /// Получаем частичный список элементов "список сериалов" с сайта.
        /// </summary>
        /// <param name="getTvSeriasListIteration">Итерация получения элемента "список сериалов" с сайта.</param>
        /// <returns>Частичный список элементов "список сериалов" с сайта.</returns>
        private List<IElement> GetListTvSeriasListElementBatch(ref int getTvSeriasListIteration)
        {
            var listTvSeriasListBatchElement = new List<IElement>();

            //Формируем список тасок на частичное получение списка элементов "список сериалов" с сайта
            var listTaskGetTvSeriasListElement = new Task<IElement>[_takeTvSeriasByRequest];
            for (int taskCounter = 0; taskCounter < _takeTvSeriasByRequest; taskCounter++)
            {
                var taskGetTvSeriasListElement =
                    GetTvSeriasListElement(getTvSeriasListIteration + (taskCounter + 1) * _takeTvSeriasByRequest);
                listTaskGetTvSeriasListElement[taskCounter] = taskGetTvSeriasListElement;
            }

            //Дожидаемся выполнение тасок
            Task.WaitAll(listTaskGetTvSeriasListElement.ToArray());

            //Формируем частичный список элементов "список сериалов" с сайта
            foreach (var taskGetTvSeriasListElement in listTaskGetTvSeriasListElement)
            {
                var tvSeriasListBatchElement = taskGetTvSeriasListElement.Result;

                if (tvSeriasListBatchElement.InnerHtml.IndexOf(_messageNotFoundByRequest, StringComparison.Ordinal) < 0)
                {
                    listTvSeriasListBatchElement.Add(tvSeriasListBatchElement);
                }
            }

            getTvSeriasListIteration = getTvSeriasListIteration + _takeTvSeriasByRequest * _takeTvSeriasByRequest;

            return listTvSeriasListBatchElement;
        }

        /// <summary>
        /// Получаем список сериалов пачками по 10 штук(больше сайт не отдает за раз) с проверкой доступности сайта.
        /// </summary>
        /// <param name="getSerialListIteration">Итерация получения списка сериалов.</param>
        /// <returns>Элемент со списком сериалов.</returns>
        private async Task<IElement> GetTvSeriasListElement(int getSerialListIteration)
        {

            IElement serialListElement = null;
            int currentRetryCount = 0;
            do
            {
                try
                {
                    //Получаем элемент со списком сериалов
                    var htmlDocumentSerialList = await _loadHtmlService.GetHtmlDocumentByUrlAsync(
                        _baseUrl + LostfilmHelper.GetAdditionalUrlToSerialList() +
                        string.Format(LostfilmHelper.GetAdditionalUrlToSerialBatchList(), getSerialListIteration));

                    serialListElement = htmlDocumentSerialList.QuerySelector(LostfilmHelper.GetTvSerailListHeaderBase());
                }
                catch (Exception e)
                {
                    //todo запись в лог о неудачной попытке обратиться к сайту
                    //todo использовать cancellationToken для таймаута
                }

                currentRetryCount++;
            } while (serialListElement == null);

            return serialListElement;
        }

        //private void ParseSerialListElement(IElement serialListElement,
        //    ConcurrentBag<TvSeriasEntity> listTvSeriasEntity)
        //{
        //    //Перебираем сериалы пока не закончились в подгруженном списке
        //    int i = 2;
        //    do
        //    {
        //        var tvSeriasEntity = new TvSeriasEntity();                

        //        //Парсим заголовок сериала из списка сериалов
        //        //возвращает false если не удалось получить следующий сериал.
        //        if (!ParseTvSeriasHeader(serialListElement, tvSeriasEntity, i)) break;

        //        //Парсим все серии и сериала.
        //        ParseOverviewTvSerial(tvSeriasEntity);

        //        //Парсим весь актерский и режисерский состав.
        //        ParseCastTvSerias(tvSeriasEntity);

        //        listTvSeriasEntity.Add(tvSeriasEntity);

        //        i = i + 2;
        //        if (i > 1000) break; //для подстраховки
        //    } while (true);
        //}

        ///// <summary>
        ///// Парсим заголовок сериала из списка сериалов.
        ///// </summary>
        ///// <param name="elSerialList">Элемент список заголовков сериалов.</param>
        ///// <param name="i">Счетчик.</param>
        ///// <returns>true - если сериал успешно получен, false - не удалось получить следующий сериал.</returns>
        //private bool ParseTvSeriasHeader(IElement elSerialList, TvSeriasEntity tvSeriasEntity, int i)
        //{
        //    var serialTopElement = elSerialList.QuerySelector(
        //        string.Format(LostfilmHelper.GetTvSerialHeader(), i));
        //    if (serialTopElement == null) return false;

        //    //Получаем элемент детализации по сериалу из заголовка
        //    var addUrlForDetailElement = serialTopElement
        //        .QuerySelector(string.Format(LostfilmHelper.GetTvSerialHeaderDetail(), i));

        //    //Получаем дополнительную ссылку для получения деталей по сериалу.
        //    tvSeriasEntity.UrlToSource = _baseUrl + addUrlForDetailElement.GetAttribute(CommonHelper.HrefAttribute);

        //    //todo сделать проверку на наличие сериала в бд по ссылке, если есть код ниже выполняться не должен
        //    //todo кроме обновление рейтинга и текущего статуса сериала!!!

        //    //Получаем ссылку на изображение-миниатюру для сериала.
        //    var imageUrlTvSerialThumbElement = serialTopElement.QuerySelector(
        //        string.Format(LostfilmHelper.GetTvSerialHeaderDetailImageUrlThumb(), i));
        //    tvSeriasEntity.PosterImageUrl =
        //        CommonHelper.HttpText + imageUrlTvSerialThumbElement.GetAttribute(CommonHelper.SrcAttribute);

        //    //Получаем название сериала по-русски.
        //    var tvSerialNameRuElement = serialTopElement.QuerySelector(
        //            string.Format(LostfilmHelper.GetTvSerialHeaderDetailTvSerialNameRu(), i));
        //    tvSeriasEntity.TitleRu = tvSerialNameRuElement?.InnerHtml;

        //    //Получаем название сериала по-английски.
        //    var tvSerialNameEnElement = serialTopElement.QuerySelector(
        //        string.Format(LostfilmHelper.GetTvSerialHeaderDetailTvSerialNameEn(), i));
        //    tvSeriasEntity.TitleEn = tvSerialNameEnElement?.InnerHtml;

        //    //Получаем рейтинг сериала на Lostfilm.
        //    var lostfilmRateElement = serialTopElement.QuerySelector(
        //        string.Format(LostfilmHelper.GetTvSerialHeaderLostfilmRate(), i));
        //    double.TryParse(lostfilmRateElement.InnerHtml, out double lostfilmRate);
        //    tvSeriasEntity.LostfilmRate = lostfilmRate;

        //    //Парсим заголовок сериала из списка сериалов панель детализации
        //    ParseTvSeriasHeaderDetailsPane(serialTopElement, tvSeriasEntity, i);

        //    //Парсим обзорную информацию по сериалу
        //    ParseTvSerialOverviewDetails(tvSeriasEntity);

        //    return true;
        //}

        ///// <summary>
        ///// Парсим заголовок сериала из списка сериалов панель детализации.
        ///// </summary>
        ///// <param name="serialTopElement">Элемент заголовка сериала.</param>
        ///// <param name="i">Счетчик елементов заголовка сериала.</param>
        //private void ParseTvSeriasHeaderDetailsPane(IElement serialTopElement,
        //    TvSeriasEntity tvSeriasEntity, int i)
        //{
        //    //Получаем панель деталей
        //    var detailsPaneElement = serialTopElement.QuerySelector(
        //        string.Format(LostfilmHelper.GetTvSerialHeaderDetailPane(), i));
        //    string detailsPane = detailsPaneElement.InnerHtml;

        //    //Получаем текущий статус сериала
        //    tvSeriasEntity.CurrentStatus =
        //        StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordStatus(),
        //            CommonHelper.OpenAngleBracket);

        //    //Получаем теливизионный канал на котором показывают сериал
        //    tvSeriasEntity.TvSerialCanal =
        //        StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordCanal(),
        //            CommonHelper.OpenAngleBracket);

        //    //Получаем список жанров в виде строки для последующего парсинга.
        //    tvSeriasEntity.ListGenreForParse =
        //        StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordGenre(),
        //            CommonHelper.OpenAngleBracket);

        //    //Получаем год начала показа сериала.
        //    tvSeriasEntity.TvSerialYearStart =
        //        StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordYearStart(),
        //            CommonHelper.OpenAngleBracket);
        //}

        ///// <summary>
        ///// Парсим обзорную информацию по сериалу
        ///// </summary>
        //private void ParseTvSerialOverviewDetails(TvSeriasEntity tvSeriasEntity)
        //{
        //    var htmlDocumentSerialOverviewDetails =
        //        _loadHtmlService.GetHtmlDocumentByUrlAsync(tvSeriasEntity.UrlToSource);

        //    var serialOverviewElement =
        //        htmlDocumentSerialOverviewDetails.QuerySelector(LostfilmHelper.GetTvSerialOverview());

        //    var serialOverviewText = serialOverviewElement.InnerHtml;

        //    //Получаем рейтинг ImDb
        //    string rateImDbText = StringHelper.GetSubstring(serialOverviewText,
        //        LostfilmHelper.GetTvSerialKeywordRateImDb(), CommonHelper.OpenAngleBracket);

        //    double.TryParse(rateImDbText, out double rateImDb);
        //    tvSeriasEntity.RateImDb = rateImDb;

        //    //Получаем дату премьеры сериала прописью
        //    var premiereDateForParseElement = serialOverviewElement.QuerySelector(
        //        LostfilmHelper.GetTvSerialPremiereDateForParse());
        //    tvSeriasEntity.PremiereDateForParse = premiereDateForParseElement?.InnerHtml; //todo распарсить

        //    //Получаем ссылку на ориганильный сайт
        //    var urlToOfficialSiteElement =
        //        serialOverviewElement.QuerySelector(LostfilmHelper.GetTvSerialUrlToOfficialSite());
        //    tvSeriasEntity.UrlToOfficialSite = urlToOfficialSiteElement?.InnerHtml;

        //    //Полчаем описание сериала
        //    var summaryElement = serialOverviewElement.QuerySelector(LostfilmHelper.GetTvSerialSummary());
        //    tvSeriasEntity.Summary = summaryElement?.InnerHtml;
        //    //todo доработать чтобы тут было только описание

        //    //todo страна

        //}

        ///// <summary>
        ///// Парсим все серии и сериала.
        ///// </summary>
        //private void ParseOverviewTvSerial(TvSeriasEntity tvSeriasEntity)
        //{
        //    try
        //    {
        //        var htmlDocumentTvSerialAllEpisodes = _loadHtmlService.GetHtmlDocumentByUrlAsync(
        //            tvSeriasEntity.UrlToSource + LostfilmHelper.AdditionalUrlToTvSerialEpisodes());

        //        var tvSerialAllEpisodesElement =
        //            htmlDocumentTvSerialAllEpisodes.QuerySelector(LostfilmHelper.GetAllEpisodes());

        //        //Получает массив постеров сезонов.
        //        var arrSeasonPosters = GetSeasonPosters(tvSerialAllEpisodesElement);

        //        //Получаем список всех эпизодов
        //        //var tvSerialAllEpisodesTableElement =
        //        //    tvSerialAllEpisodesElement.GetElementsByTagName(CommonHelper.TbodyAttribute).First();
        //        var listTvSerialAllEpisodes =
        //            tvSerialAllEpisodesElement.GetElementsByTagName(CommonHelper.TrAttribute);

        //        foreach (var episodeElement in listTvSerialAllEpisodes)
        //        {
        //            //Получаем номер сериала и номер сезона.
        //            GetSerialNumberAndEpisodeNumber(episodeElement, out int seasonsNumber, out int episodeNumber);

        //            //Получаем сущность сезона.
        //            var seasonEntity = GetSeasonEntity(tvSeriasEntity, seasonsNumber, arrSeasonPosters);

        //            //Получаем сущность серии
        //            var episodeEntity = GetEpisodeEntity(tvSeriasEntity, seasonsNumber, episodeNumber);

        //            //Получаем ссылку на серию.
        //            GetUrlForEpisodeSource(episodeElement, episodeEntity);

        //            //Получаем название серии (русское и английское).
        //            GetEpisodeName(episodeElement, episodeEntity);

        //            //Получаем даты релизов (русская версия и английская).
        //            GetReleaseDate(episodeElement, episodeEntity);

        //            seasonEntity.ListEpisode.Add(episodeEntity);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //}

        ///// <summary>
        ///// Получает массив постеров сезонов.
        ///// </summary>
        ///// <param name="tvSerialAllEpisodesElement">Элемент содержащий все серии.</param>
        ///// <returns>Массив постеров сезонов.</returns>
        //private string[] GetSeasonPosters(IElement tvSerialAllEpisodesElement)
        //{
        //    var seasonPosters = tvSerialAllEpisodesElement.GetElementsByClassName(LostfilmHelper.GetSeasonPosters());
        //    int countSeasons = seasonPosters.Length;
        //    string[] arrSeasonPosters = new string[seasonPosters.Length];
        //    int i = 1;
        //    foreach (var seasonPoster in seasonPosters)
        //    {
        //        arrSeasonPosters[countSeasons - i] = 
        //            CommonHelper.HttpText + StringHelper.GetSubstring(
        //                seasonPoster.OuterHtml, CommonHelper.Apostrophe, CommonHelper.Apostrophe);
        //        i++;
        //    }

        //    return arrSeasonPosters;
        //}

        ///// <summary>
        ///// Получаем сущность сезона.
        ///// </summary>
        ///// <param name="seasonsNumber">Номер сезона</param>
        ///// <param name="arrSeasonPosters">массив постеров сезонов.</param>
        ///// <returns>сущность сезона.</returns>
        //private SeasonEntity GetSeasonEntity(TvSeriasEntity tvSeriasEntity, int seasonsNumber, string[] arrSeasonPosters)
        //{
        //    var seasonEntity = tvSeriasEntity.ListSeasons
        //        .FirstOrDefault(item => item.Number == seasonsNumber);
        //    if (seasonEntity == null)
        //    {
        //        seasonEntity = new SeasonEntity
        //        {
        //            ListEpisode = new List<EpisodeEntity>(),
        //            Number = seasonsNumber
        //        };

        //        if (arrSeasonPosters.Length >= seasonsNumber)
        //        {
        //            seasonEntity.PosterImageUrl = arrSeasonPosters[seasonsNumber - 1];
        //        }

        //        tvSeriasEntity.ListSeasons.Add(seasonEntity);
        //    }

        //    return seasonEntity;
        //}

        ///// <summary>
        ///// Получаем номер сериала и номер сезона.
        ///// </summary>
        ///// <param name="episodeElement">Элемент серии.</param>
        ///// <param name="seasonsNumber">Номер сезона(возвращаемое).</param>
        ///// <param name="episodeNumber">Номер сериала(возвращаемое).</param>
        //private void GetSerialNumberAndEpisodeNumber(IElement episodeElement, out int seasonsNumber, out int episodeNumber)
        //{
        //    var episodeAndSeriaNumberForParse = episodeElement
        //        .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameBeta()).First().InnerHtml;

        //    var seasonNumberText = StringHelper.GetSubstring(
        //        episodeAndSeriaNumberForParse, string.Empty, LostfilmHelper.GetSeasonKeyword());
        //    var episodeNumberText = StringHelper.GetSubstring(episodeAndSeriaNumberForParse,
        //        LostfilmHelper.GetSeasonKeyword(), LostfilmHelper.GetEpisodeKeyword());

        //    int.TryParse(seasonNumberText, out int seasonsNumberRes);
        //    int.TryParse(episodeNumberText, out int episodeNumberRes);

        //    seasonsNumber = seasonsNumberRes;
        //    episodeNumber = episodeNumberRes;
        //}

        ///// <summary>
        ///// Получаем сущность серии.
        ///// </summary>
        ///// <param name="seasonsNumber">Номер сезона</param>
        ///// <param name="episodeNumber">Номер серии.</param>
        ///// <returns>сущность серии.</returns>
        //private EpisodeEntity GetEpisodeEntity(TvSeriasEntity tvSeriasEntity, int seasonsNumber, int episodeNumber)
        //{
        //    var episodeEntity = tvSeriasEntity.ListSeasons
        //        .First(item => item.Number == seasonsNumber)
        //        .ListEpisode
        //        .FirstOrDefault(item => item.Number == episodeNumber);

        //    if (episodeEntity != null) return episodeEntity;

        //    episodeEntity = new EpisodeEntity
        //    {
        //        Number = episodeNumber
        //    };

        //    return episodeEntity;
        //}

        ///// <summary>
        ///// Получаем ссылку на серию.
        ///// </summary>
        ///// <param name="episodeElement">Элемент серии.</param>
        ///// <param name="episodeEntity">Сущность эпизода.</param>
        //private void GetUrlForEpisodeSource(IElement episodeElement, EpisodeEntity episodeEntity)
        //{
        //    string outerHtmlBetaClass = episodeElement
        //        .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameBeta()).First().OuterHtml;
        //    episodeEntity.UrlForEpisodeSource =
        //        LostfilmHelper.GetBaseUrl() + StringHelper.GetSubstring(
        //            outerHtmlBetaClass, CommonHelper.Apostrophe, CommonHelper.Apostrophe);
        //}

        ///// <summary>
        ///// Получаем название серии (русское и английское).
        ///// </summary>
        ///// <param name="episodeElement">Элемент серии.</param>
        ///// <param name="episodeEntity">Сущность эпизода.</param>
        //private void GetEpisodeName(IElement episodeElement, EpisodeEntity episodeEntity)
        //{
        //    var gammaClassElement = episodeElement
        //        .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameGamma()).First();
        //    string someTextForParseName = gammaClassElement.GetElementsByTagName(CommonHelper.DivAttribute)
        //        .FirstOrDefault()?.InnerHtml;
        //    if (!string.IsNullOrEmpty(someTextForParseName))
        //    {
        //        episodeEntity.TitleRu = StringHelper.GetSubstring(someTextForParseName,
        //            string.Empty, CommonHelper.OpenAngleBracket, false);
        //        episodeEntity.TitleEn = episodeElement
        //            .GetElementsByClassName(LostfilmHelper.GetGrayColor2SmallTextClass())
        //            .First()
        //            .InnerHtml;
        //    }
        //    else
        //    {
        //        someTextForParseName = gammaClassElement.OuterHtml;
        //        episodeEntity.TitleRu = StringHelper.GetSubstring(someTextForParseName,
        //            CommonHelper.CloseAngleBracket, CommonHelper.BrAttribute + CommonHelper.CloseAngleBracket, false);
        //        episodeEntity.TitleEn = episodeElement
        //            .GetElementsByClassName(LostfilmHelper.GetSmallTextClass())
        //            .First()
        //            .InnerHtml;
        //    }
        //}

        ///// <summary>
        ///// Получаем даты релизов (русская версия и английская).
        ///// </summary>
        ///// <param name="episodeElement">Элемент серии.</param>
        ///// <param name="episodeEntity">Сущность эпизода.</param>
        //private void GetReleaseDate(IElement episodeElement, EpisodeEntity episodeEntity)
        //{
        //    string someTextForParseDates = episodeElement
        //        .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameDelta())
        //        .First()
        //        .OuterHtml;

        //    string releaseDateRuText = StringHelper.GetSubstring(
        //        someTextForParseDates, LostfilmHelper.GetRuKeyword(), CommonHelper.OpenAngleBracket);
        //    DateTime.TryParseExact(releaseDateRuText, LostfilmHelper.GetDateFormat(),
        //        new CultureInfo(CommonHelper.CultureInfoText), DateTimeStyles.None, out DateTime releaseDateRu);
        //    episodeEntity.ReleaseDateRu = releaseDateRu;

        //    string releaseDateEnText = StringHelper.GetSubstring(
        //        someTextForParseDates, LostfilmHelper.GetEngKeyword(), CommonHelper.OpenAngleBracket);
        //    DateTime.TryParseExact(releaseDateEnText, LostfilmHelper.GetDateFormat(),
        //        new CultureInfo(CommonHelper.CultureInfoText), DateTimeStyles.None, out DateTime releaseDateEn);
        //    episodeEntity.ReleaseDateEn = releaseDateEn;
        //}

        ///// <summary>
        ///// Парсим весь актерский и режисерский состав.
        ///// </summary>
        //private void ParseCastTvSerias(TvSeriasEntity tvSeriasEntity)
        //{
        //    var htmlDocumentCastTvSerias = _loadHtmlService.GetHtmlDocumentByUrlAsync(
        //        tvSeriasEntity.UrlToSource + LostfilmHelper.AdditionalUrlToTvSerialCasts());

        //    int i = 1;
        //    string castType = string.Empty;
        //    do
        //    {
        //        //Получаем элемент актерского состава.
        //        var castElement = GetCastElement(htmlDocumentCastTvSerias, i);
        //        if (castElement == null) break;

        //        if (LostfilmHelper.GetClassRow() == castElement.ClassName)
        //        {
        //            //Создаем сущность персоны.
        //            var personEntity = CreatePersonEntity(castElement);

        //            //Добавляем в сущность хранения данных о сериале персону.
        //            AddPersonEntityToTvSerias(tvSeriasEntity, personEntity, castType);
        //        }
        //        else
        //        {
        //            if (LostfilmHelper.GetClassHeaderSimple() == castElement.ClassName)
        //            {
        //                castType = castElement.InnerHtml;
        //            }
        //        }

        //        i++;
        //    } while (true);
        //}

        ///// <summary>
        ///// Получаем элемент актерского состава.
        ///// </summary>
        ///// <param name="htmlDocumentCastTvSerias">htmlDocument актерского состава.</param>
        ///// <param name="i">Счетчик.</param>
        ///// <returns>Элемент актерского состава.</returns>
        //private IElement GetCastElement(IHtmlDocument htmlDocumentCastTvSerias, int i)
        //{
        //    var castElement = htmlDocumentCastTvSerias.QuerySelector(
        //        string.Format(LostfilmHelper.GetSelectorCastElement(), i));

        //    if (castElement == null)
        //    {
        //        castElement = htmlDocumentCastTvSerias.QuerySelector(
        //            string.Format(LostfilmHelper.GetSelectorHrefCastElement(), i));
        //    }

        //    return castElement;
        //}

        ///// <summary>
        ///// Создаем сущность персоны.
        ///// </summary>
        ///// <param name="castElement">Элемент актерского состава.</param>
        ///// <returns>Сущность персоны.</returns>
        //private PersonEntity CreatePersonEntity(IElement castElement)
        //{
        //    var personEntity = new PersonEntity();

        //    personEntity.LostfilmPersonalPageUrl =
        //        LostfilmHelper.GetBaseUrl() + castElement.GetAttribute(CommonHelper.HrefAttribute);

        //    var aloadThumbElement = castElement
        //        .GetElementsByClassName(LostfilmHelper.GetClassAloadThumb())
        //        .FirstOrDefault();

        //    if (aloadThumbElement != null)
        //    {
        //        personEntity.PhotoThumbnailUrl = 
        //            CommonHelper.HttpText + aloadThumbElement.GetAttribute(CommonHelper.AutoloadAttribute); 
        //    }

        //    personEntity.FullNameRu = castElement
        //        .GetElementsByClassName(LostfilmHelper.GetClassNameRu())
        //        .First()
        //        .InnerHtml;
        //    personEntity.FullNameEn = castElement
        //        .GetElementsByClassName(LostfilmHelper.GetClassNameEn())
        //        .First()
        //        .InnerHtml;

        //    return personEntity;
        //}

        ///// <summary>
        ///// Добавляем в сущность хранения данных о сериале персону.
        ///// </summary>
        ///// <param name="personEntity">Сущность персоны.</param>
        ///// <param name="castType">Тип персоны.</param>
        //private void AddPersonEntityToTvSerias(TvSeriasEntity tvSeriasEntity, PersonEntity personEntity,
        //    string castType)
        //{
        //    //Если актер добавляем в список актеров
        //    if (castType == LostfilmHelper.GetKeywordActors())
        //    {
        //        tvSeriasEntity.ListActor.Add(personEntity);
        //        return;
        //    }

        //    //Если режисер добавляем в список режисеров
        //    if (castType == LostfilmHelper.GetKeywordDirectors())
        //    {
        //        tvSeriasEntity.ListDirector.Add(personEntity);
        //        return;
        //    }

        //    //Если продюссер добавляем в список продюссеров
        //    if (castType == LostfilmHelper.GetKeywordProducers())
        //    {
        //        tvSeriasEntity.ListProducer.Add(personEntity);
        //        return;
        //    }

        //    //Если сценарист добавляем в список сценаристов
        //    if (castType == LostfilmHelper.GetKeywordWriters())
        //    {
        //        tvSeriasEntity.ListWriter.Add(personEntity);
        //        return;
        //    }
        //}
    }
}
