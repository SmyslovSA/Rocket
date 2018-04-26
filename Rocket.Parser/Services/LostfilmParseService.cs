using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
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

                    if (lostfilmSerialModel.NewSeriaDetailNewUrl != null)
                    {
                        //Получаем элемент с более подробной информацией о новой серии
                        var htmlDetailNewSeria = await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + lostfilmSerialModel.NewSeriaDetailNewUrl);

                        //Парсим информацию о новой серии с её страницы
                        ParseDetailNewSeria(htmlDetailNewSeria, lostfilmSerialModel);
                    }

                    listLostfilmSerialModel.Add(lostfilmSerialModel);

                    i = i + 2;
                }
                while (true || i < 2000); //на всякий случай предусмотрим выход из цикла после обработки 1000 сериалов

                //todo сделать чтобы догрузилось еще 10 строк

                //todo сделать запихивание данных в бд

                //todo сделать получение Person


                var t = 1211;

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
        public void ParseSerialHeaderBase(IElement serialTopElement, LostfilmSerialModel lostfilmSerialModel, int i)
        {
            var addUrlForDetailElement = serialTopElement.QuerySelector($"#serials_list > div:nth-child({i}) > a");
            lostfilmSerialModel.AddUrlForDetail = addUrlForDetailElement.GetAttribute("href");

            var imageUrlTvSerialThumbElement =
                serialTopElement.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.picture-box > img.thumb");
            lostfilmSerialModel.ImageUrlTvSerialThumb = "http:" + imageUrlTvSerialThumbElement.GetAttribute("src");

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
        public void ParseSerialHeaderDetailsPane(IElement serialTopElement, LostfilmSerialModel lostfilmSerialModel, int i)
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

        public string GetDetailsElement(string detailsText, string keyword, string endString)
        {
            if (endString == null) throw new ArgumentNullException(nameof(endString));

            int startIndex = detailsText.IndexOf(keyword, StringComparison.Ordinal) + keyword.Length;

            int endIndex = detailsText.IndexOf(endString, startIndex, StringComparison.Ordinal);
            if (endIndex < 0) endIndex = detailsText.Length;

            string currentDetailsPane = detailsText.Substring(startIndex, endIndex - startIndex);

            return Regex.Replace(currentDetailsPane, @"[ \t\n\r\f\v]", "");
        }

        public void ParseSerialOverviewDetails(IHtmlDocument htmlDocumentSerialListDetail, LostfilmSerialModel lostfilmSerialModel)
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
            lostfilmSerialModel.OfficialSite = officialSiteElement?.InnerHtml;
        }

        public void ParseOverviewNewSeria(IHtmlDocument htmlDocumentSerialOverviewDetails, LostfilmSerialModel lostfilmSerialModel)
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
                lostfilmSerialModel.NewSeriaDetailNewUrl = seriaDetailNewUrl.Substring(6, indexEnd - indexStart);
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

        public void ParseDetailNewSeria(IHtmlDocument htmlDetailNewSeria, LostfilmSerialModel lostfilmSerialModel)
        {
            var durationInMinElement = htmlDetailNewSeria.QuerySelector(
                "#left-pane > div.white-background.clearfix > div:nth-child(5) > div.details-pane > div:nth-child(2)");
            string durationInMinText = durationInMinElement.InnerHtml;

            string keyword = "Длительность:";
            durationInMinText = GetDetailsElement(durationInMinText, keyword, "<");
            durationInMinText = Regex.Replace(durationInMinText, @"[а-я]", "");
            double.TryParse(durationInMinText, out double durationInMin);
            lostfilmSerialModel.DurationInMin = durationInMin;
        }
    }
}
