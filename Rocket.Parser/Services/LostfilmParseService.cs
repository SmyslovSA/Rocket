using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Rocket.DAL.Common.DbModels.Parser;
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

        //todo вынести в хэлпер
        private const string AddCastUrl = "/cast";
        private const string HttpPrefix = "http:";
        private const string SeasonText = "сезон";
        private const string EpisodeText = "серия";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loadHtmlService">Сервис загрузки HTML</param>

        public LostfilmParseService(ILoadHtmlService loadHtmlService)
        {
            _loadHtmlService = loadHtmlService;
        }

        public async Task ParseAsync()
        {
            try
            {
                //todo эта настройка должна лежать в базе и задаваться через админку на UI, а пока в конфиге
                //Получаем основную ссылку на ресурс LostFilm
                var baseUrl = System.Configuration.ConfigurationManager.AppSettings["LostfilmParseBaseUrl"];

                //Получаем элемент со списком сериалов
                var htmlDocumentSerialList = await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + "/series");
                var elSerialList = htmlDocumentSerialList.QuerySelector("#serials_list");

                //Формируем список моделей с данными которые нам удалось вытянуть с Lostfilm (грузит по 10 сериалов за раз)
                var listLostfilmSerialModel = new List<LostfilmSerialModel>();
                int i = 2;
                IElement serialTopHtml;
                do
                {
                    //Перебираем сериалы пока не закончились в подгруженном списке
                    serialTopHtml = elSerialList.QuerySelector($"#serials_list > div:nth-child({i})");
                    if (serialTopHtml == null) break;

                    var lostfilmSerialModel = new LostfilmSerialModel();

                    //Парсим заголовок сериала из списка сериалов
                    ParseSerialHeaderBase(serialTopHtml, lostfilmSerialModel, i);

                    //Парсим заголовок сериала из списка сериалов панель детализации
                    ParseSerialHeaderDetailsPane(serialTopHtml, lostfilmSerialModel, i);

                    //Получаем элемент с обзорной информацией о сериале
                    var htmlDocumentSerialOverviewDetails =
                        await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + lostfilmSerialModel.AddUrlForDetail);

                    //Парсим обзорную информацию по сериалу
                    ParseSerialOverviewDetails(htmlDocumentSerialOverviewDetails, lostfilmSerialModel);

                    //Парсим обзорную информации по еще не вышедшей серии
                    ParseOverviewNewSeria(htmlDocumentSerialOverviewDetails, lostfilmSerialModel);

                    if (lostfilmSerialModel.NewSeriaDetailUrl != null)
                    {
                        //Получаем элемент с более подробной информацией о новой серии
                        var htmlDetailNewSeria = await _loadHtmlService.GetHtmlDocumentByUrlAsync(
                            baseUrl + lostfilmSerialModel.NewSeriaDetailUrl);

                        //Парсим информацию о новой серии с её страницы
                        ParseDetailNewSeria(htmlDetailNewSeria, lostfilmSerialModel);
                    }

                    //Получаем элемент со списком актеров режисеров продюсеров
                    var htmlCastTvSerial = await _loadHtmlService.GetHtmlDocumentByUrlAsync(
                        baseUrl + lostfilmSerialModel.AddUrlForDetail + AddCastUrl);

                    //Парсим информацию о новой серии с её страницы
                    ParseCastTvSerial(htmlCastTvSerial, lostfilmSerialModel);

                    listLostfilmSerialModel.Add(lostfilmSerialModel);

                    i = i + 2;
                }
                while (true || i < 2000); //на всякий случай предусмотрим выход из цикла после обработки 1000 сериалов

                //todo сделать чтобы догрузилось еще 10 строк


                //todo сделать через автомапер
                foreach (var lostfilmSerialModel in listLostfilmSerialModel)
                {
                    //todo добавить в сущности поля которые я уже вытянул(рейтинг, ссылка на оф сайт и т.д.)
                    //создаем сущность сериала
                    var tvSeriasEntity = CreateTvSeriasEntity(lostfilmSerialModel);

                    //todo тут делаем Save без комита

                    //создаем сущность сезона
                    var seasonEntity = CreateSeasonEntity(lostfilmSerialModel, tvSeriasEntity);

                    // todo тут делаем Save без комита

                    //создаем сущность серии
                    var episodeEntity = CreateEpisodeEntity(lostfilmSerialModel, seasonEntity);

                    // todo тут делаем Save с комитом
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Парсим заголовок сериала из списка сериалов.
        /// </summary>
        /// <param name="serialTopElement">Элемент заголовка сериала.</param>
        /// <param name="lostfilmSerialModel">Модель для временной агрегации данных результата парсинга.</param>
        /// <param name="i">Счетчик.</param>
        private void ParseSerialHeaderBase(IElement serialTopElement, LostfilmSerialModel lostfilmSerialModel, int i)
        {
            var addUrlForDetailElement = serialTopElement.QuerySelector($"#serials_list > div:nth-child({i}) > a");
            lostfilmSerialModel.AddUrlForDetail = addUrlForDetailElement.GetAttribute("href");

            var imageUrlTvSerialThumbElement =
                serialTopElement.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.picture-box > img.thumb");
            lostfilmSerialModel.ImageUrlTvSerialThumb = HttpPrefix + imageUrlTvSerialThumbElement.GetAttribute("src");

            var tvSerialNameRuElement =
                serialTopElement.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.body > div.name-ru");
            lostfilmSerialModel.TvSerialNameRu = tvSerialNameRuElement?.InnerHtml;

            var tvSerialNameEnElement =
                serialTopElement.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.body > div.name-en");
            lostfilmSerialModel.TvSerialNameEn = tvSerialNameEnElement?.InnerHtml;

            var lostfilmRateElement =
                serialTopElement.QuerySelector($"#serials_list > div:nth-child({i}) > div.mark-green-box");

            double.TryParse(lostfilmRateElement.InnerHtml, out double lostfilmRate);
            lostfilmSerialModel.TvSerialRateOnLostFilm = lostfilmRate;
        }

        /// <summary>
        /// Парсим заголовок сериала из списка сериалов панель детализации.
        /// </summary>
        /// <param name="serialTopElement">Элемент заголовка сериала.</param>
        /// <param name="lostfilmSerialModel">Модель для временной агрегации данных результата парсинга.</param>
        /// <param name="i">Счетчик.</param>
        private void ParseSerialHeaderDetailsPane(IElement serialTopElement, LostfilmSerialModel lostfilmSerialModel, int i)
        {
            var detailsPaneElement = serialTopElement.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.body > div.details-pane");
            string detailsPane = detailsPaneElement.InnerHtml;

            lostfilmSerialModel.TvSerialCurrentStatus =
                GetDetailsElement(detailsPane, LostFilmSerailListHelper.GetKeywordStatus(), "<");

            lostfilmSerialModel.TvSerialCanal =
                GetDetailsElement(detailsPane, LostFilmSerailListHelper.GetKeywordCanal(), "<");

            lostfilmSerialModel.ListGenreForParse =
                GetDetailsElement(detailsPane, LostFilmSerailListHelper.GetKeywordGenre(), "<");
            
            lostfilmSerialModel.TvSerialYearStart =
                GetDetailsElement(detailsPane, LostFilmSerailListHelper.GetKeywordYearStart(), "<");
        }

        private string GetDetailsElement(string detailsText, string keyword, string endString)
        {
            if (endString == null) throw new ArgumentNullException(nameof(endString));

            int startIndex = detailsText.IndexOf(keyword, StringComparison.Ordinal) + keyword.Length;

            int endIndex = detailsText.IndexOf(endString, startIndex, StringComparison.Ordinal);
            if (endIndex < 0) endIndex = detailsText.Length;

            string currentDetailsPane = detailsText.Substring(startIndex, endIndex - startIndex);

            return Regex.Replace(currentDetailsPane, @"[ \t\n\r\f\v]", "");
        }

        private void ParseSerialOverviewDetails(IHtmlDocument htmlDocumentSerialListDetail, LostfilmSerialModel lostfilmSerialModel)
        {
            var serialOverviewElement =
                htmlDocumentSerialListDetail.QuerySelector("#left-pane > div:nth-child(5) > div.details-pane");

            var premiereDateTextElement = serialOverviewElement.QuerySelector(
                "#left-pane > div:nth-child(5) > div.details-pane > div.left-box > a:nth-child(1)");
            lostfilmSerialModel.PremiereDateText = premiereDateTextElement?.InnerHtml;

            var serialOverviewText = serialOverviewElement.InnerHtml;
            string keyword = "Рейтинг IMDb:";

            string rateImDbText = GetDetailsElement(serialOverviewText, keyword, "<");
            double.TryParse(rateImDbText, out double rateImDb);

            lostfilmSerialModel.RateImDb = rateImDb;

            var officialSiteElement =
                serialOverviewElement.QuerySelector(
                    "#left-pane > div:nth-child(5) > div.details-pane > div.right-box > a:nth-child(8)");
            lostfilmSerialModel.OfficialSiteUrl = officialSiteElement?.InnerHtml;
        }

        private void ParseOverviewNewSeria(IHtmlDocument htmlDocumentSerialOverviewDetails, LostfilmSerialModel lostfilmSerialModel)
        {
            var serialListDetailElement =
                htmlDocumentSerialOverviewDetails.QuerySelector("#left-pane > div.text-block.guide");

            var episodeAndSerialNumberElement =
                htmlDocumentSerialOverviewDetails.QuerySelector("#left-pane > div.text-block.guide > div.body > table > tbody > tr > td.beta");
            lostfilmSerialModel.EpisodeAndSeriaNumberText = episodeAndSerialNumberElement?.InnerHtml;

            var serialDetailElementNew =
                serialListDetailElement.QuerySelector("#left-pane > div.text-block.guide > div.body > table > tbody > tr.not-available > td.beta");

            if (serialDetailElementNew != null)
            {
                string seriaDetailNewUrl = serialDetailElementNew.GetAttribute("onclick");
                int indexStart = seriaDetailNewUrl.IndexOf("'", StringComparison.Ordinal);
                int indexEnd = seriaDetailNewUrl.IndexOf("'", indexStart + 1, StringComparison.Ordinal) - 1;
                lostfilmSerialModel.NewSeriaDetailUrl = seriaDetailNewUrl.Substring(6, indexEnd - indexStart);
            }

            var newSeriaDateReleaseRu =
                serialListDetailElement.QuerySelector("#left-pane > div.text-block.guide > div.body > table > tbody > tr > td.delta");

            var newSeriaDateReleaseText = newSeriaDateReleaseRu.InnerHtml;

            string keyword = "Ru:";
            string dateReleaseRuText = GetDetailsElement(newSeriaDateReleaseText, keyword, "<");
            DateTime.TryParse(dateReleaseRuText, out DateTime dateReleaseRu);
            lostfilmSerialModel.DateReleaseRu = dateReleaseRu;

            keyword = "Eng:";
            string dateReleaseEnText = GetDetailsElement(newSeriaDateReleaseText, keyword, "<");
            DateTime.TryParse(dateReleaseEnText, out DateTime dateReleaseEn);
            lostfilmSerialModel.DateReleaseEn = dateReleaseEn;
        }

        private void ParseDetailNewSeria(IHtmlDocument htmlDetailNewSeria, LostfilmSerialModel lostfilmSerialModel)
        {
            var durationInMinElement = htmlDetailNewSeria.QuerySelector(
                "#left-pane > div.white-background.clearfix > div:nth-child(5) > div.details-pane > div:nth-child(2)");
            string durationInMinText = durationInMinElement.InnerHtml;

            string keyword = "Длительность:";
            durationInMinText = GetDetailsElement(durationInMinText, keyword, "<");
            durationInMinText = Regex.Replace(durationInMinText, @"[а-я]", "");
            int.TryParse(durationInMinText, out int durationInMin);
            lostfilmSerialModel.DurationInMin = durationInMin;
        }

        private void ParseCastTvSerial(IHtmlDocument htmlCastTvSerial, LostfilmSerialModel lostfilmSerialModel)
        {
            int i = 4;
            lostfilmSerialModel.ListActor = ParseCastListOfPerson(htmlCastTvSerial, ref i);

            i = i + 2;
            lostfilmSerialModel.ListDirector = ParseCastListOfPerson(htmlCastTvSerial, ref i);

            i = i + 2;
            lostfilmSerialModel.ListProducer = ParseCastListOfPerson(htmlCastTvSerial, ref i);

            i = i + 2;
            lostfilmSerialModel.ListWriter = ParseCastListOfPerson(htmlCastTvSerial, ref i);
        }

        private List<PersonEntity> ParseCastListOfPerson(IHtmlDocument htmlCastTvSerial, ref int i)
        {
            var listPersonEntity = new List<PersonEntity>();

            IElement personElement;
            do
            {
                personElement = htmlCastTvSerial.QuerySelector(
                    $"#left-pane > div.content > div.center-block.margin-left > div > div > a:nth-child({i})");

                if (personElement != null)
                {
                    var personEntity = new PersonEntity();
                    var actorImgElement = htmlCastTvSerial.QuerySelector(
                        $"#left-pane > div.content > div.center-block.margin-left > div > div > a:nth-child({i}) > img");

                    string photoThumbnailUrl = actorImgElement.GetAttribute("autoload");
                    if (!string.IsNullOrEmpty(photoThumbnailUrl))
                    {
                        personEntity.PhotoThumbnailUrl = HttpPrefix + photoThumbnailUrl;
                    }
                    personEntity.FullNameRu = personElement.GetElementsByClassName("name-ru").FirstOrDefault()?.InnerHtml;
                    personEntity.FullNameEn = personElement.GetElementsByClassName("name-en").FirstOrDefault()?.InnerHtml;

                    listPersonEntity.Add(personEntity);
                    i = i + 2;
                }
            } while (personElement != null || i > 200); //предусматриваем "запасной выход"

            return listPersonEntity;
        }

        private TvSeriasEntity CreateTvSeriasEntity(LostfilmSerialModel lostfilmSerialModel)
        {
            //todo проверять на существование через совпадение по имени

            var tvSeriasEntity = new TvSeriasEntity();

            tvSeriasEntity.TitleRu = lostfilmSerialModel.TvSerialNameRu;
            tvSeriasEntity.TitleEn = lostfilmSerialModel.TvSerialNameEn;
            tvSeriasEntity.PosterImageUrl = lostfilmSerialModel.ImageUrlTvSerialThumb;
            tvSeriasEntity.Summary = null; //todo вытянуть парсером
            tvSeriasEntity.ListActor = lostfilmSerialModel.ListActor;
            tvSeriasEntity.ListDirector = lostfilmSerialModel.ListDirector;
            tvSeriasEntity.ListProducer = lostfilmSerialModel.ListProducer;
            tvSeriasEntity.ListWriter = lostfilmSerialModel.ListWriter;
            //todo распарсить и подставить жанры, если их нет добавить в бд
            tvSeriasEntity.ListGenreEntity = null;

            return tvSeriasEntity;
        }

        private SeasonEntity CreateSeasonEntity(LostfilmSerialModel lostfilmSerialModel,
            TvSeriasEntity tvSeriasEntity)
        {

            //todo проверять на существование через совпадение по имени

            var seasonEntity = new SeasonEntity();

            string episodeAndSeriaNumberText = lostfilmSerialModel.EpisodeAndSeriaNumberText;

            int endIndex = episodeAndSeriaNumberText.IndexOf(SeasonText, StringComparison.Ordinal);
            string seasonNumberText =
                episodeAndSeriaNumberText.Substring(0, endIndex);
            seasonNumberText = Regex.Replace(seasonNumberText, @"[ ]", "");

            int.TryParse(seasonNumberText, out int seasonNumber);

            seasonEntity.Number = seasonNumber;
            seasonEntity.PosterImageUrl = null; //todo получить парсером
            seasonEntity.Summary = null; //todo получить парсером
            seasonEntity.DbTvSeriesId = tvSeriasEntity.Id;

            return seasonEntity;
        }

        private EpisodeEntity CreateEpisodeEntity(LostfilmSerialModel lostfilmSerialModel, SeasonEntity seasonEntity)
        {

            //todo проверять на существование через совпадение по имени

            var episodeEntity = new EpisodeEntity();

            string episodeAndSeriaNumberText = lostfilmSerialModel.EpisodeAndSeriaNumberText;

            int startIndex = episodeAndSeriaNumberText.IndexOf(SeasonText, StringComparison.Ordinal) 
                             + SeasonText.Length;
            int endIndex = episodeAndSeriaNumberText.IndexOf(EpisodeText, StringComparison.Ordinal);
            string episodeNumberText =
                episodeAndSeriaNumberText.Substring(startIndex, endIndex - startIndex);

            episodeNumberText = Regex.Replace(episodeNumberText, @"[ ]", "");
            int.TryParse(episodeNumberText, out int episodeNumber);

            episodeEntity.Number = episodeNumber;
            episodeEntity.ReleaseDateRu = lostfilmSerialModel.DateReleaseRu;
            episodeEntity.ReleaseDateEn = lostfilmSerialModel.DateReleaseEn;
            episodeEntity.Title = null; //todo получить парсером
            episodeEntity.Duration = new TimeSpan(0, 0, lostfilmSerialModel.DurationInMin, 0);
            episodeEntity.Summary = null;//todo получить парсером
            episodeEntity.DbSeasonId = seasonEntity.Id;

            return episodeEntity;
        }
    }
}
