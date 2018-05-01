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

        public LostfilmParseService(ILoadHtmlService loadHtmlService)
        {
            _loadHtmlService = loadHtmlService;
        }

        public void Parse()
        {
            try
            {
                ParseLostFilmAsync();
            }
            catch (Exception e)
            {
                //todo добавить логирование
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task ParseLostFilmAsync()
        {

            try
            {
                //todo эта настройка должна лежать в базе и задаваться через админку на UI, а пока в конфиге
                //Получаем основную ссылку на ресурс LostFilm
                var baseUrl = LostfilmHelper.GetBaseUrl();

                //Получаем элемент со списком сериалов
                var htmlDocumentSerialList = await _loadHtmlService.GetHtmlDocumentByUrlAsync(
                    baseUrl + LostfilmHelper.GetAdditionalUrlToSerialList());
                var elSerialList = htmlDocumentSerialList.QuerySelector(LostfilmHelper.GetTvSerailListHeaderBase());

                //Формируем список моделей с данными которые нам удалось вытянуть с Lostfilm (грузит по 10 сериалов за раз)
                var listLostfilmSerialModel = new List<LostfilmSerialModel>();
                int i = 2;
                IElement serialTopElement;
                do
                {
                    //Перебираем сериалы пока не закончились в подгруженном списке
                    serialTopElement = elSerialList.QuerySelector(
                        string.Format(LostfilmHelper.GetTvSerialHeader(), i));
                    if (serialTopElement == null)
                    {
                        //todo запись в лог - предупреждение
                        break;
                    }

                    var lostfilmSerialModel = new LostfilmSerialModel();

                    //Парсим заголовок сериала из списка сериалов
                    ParseSerialHeader(serialTopElement, lostfilmSerialModel, i);

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
        private void ParseSerialHeader(IElement serialTopElement, LostfilmSerialModel lostfilmSerialModel, int i)
        {
            //Получаем элемент детализации по сериалу из заголовка
            var addUrlForDetailElement = serialTopElement
                .QuerySelector(string.Format(LostfilmHelper.GetTvSerialHeaderDetail(), i));

            //Получаем дополнительную ссылку для получения деталей по сериалу.
            lostfilmSerialModel.AddUrlForDetail = addUrlForDetailElement.GetAttribute(CommonHelper.HrefAttribute);

            //todo сделать проверку на наличие сериала в бд по ссылке, если есть код ниже выполняться не должен
            //todo кроме обновление рейтинга!!!

            //Получаем ссылку на изображение-миниатюру для сериала.
            var imageUrlTvSerialThumbElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderDetailImageUrlThumb(), i));
            lostfilmSerialModel.ImageUrlTvSerialThumb =
                CommonHelper.HttpText + imageUrlTvSerialThumbElement.GetAttribute(CommonHelper.SrcAttribute);

            //Получаем название сериала по-русски.
            var tvSerialNameRuElement = serialTopElement.QuerySelector(
                    string.Format(LostfilmHelper.GetTvSerialHeaderDetailTvSerialNameRu(), i));
            lostfilmSerialModel.TvSerialNameRu = tvSerialNameRuElement?.InnerHtml;

            //Получаем название сериала по-английски.
            var tvSerialNameEnElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderDetailTvSerialNameEn(), i));
            lostfilmSerialModel.TvSerialNameEn = tvSerialNameEnElement?.InnerHtml;

            //Получаем рейтинг сериала на Lostfilm.
            var lostfilmRateElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderLostfilmRate(), i));
            double.TryParse(lostfilmRateElement.InnerHtml, out double lostfilmRate);
            lostfilmSerialModel.TvSerialRateOnLostFilm = lostfilmRate;

            //Парсим заголовок сериала из списка сериалов панель детализации
            ParseSerialHeaderDetailsPane(serialTopElement, lostfilmSerialModel, i);
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
            lostfilmSerialModel.TvSerialCurrentStatus =
                StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordStatus(),
                    CommonHelper.OpenAngleBracket);

            //Получаем теливизионный канал на котором показывают сериал
            lostfilmSerialModel.TvSerialCanal =
                StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordCanal(),
                    CommonHelper.OpenAngleBracket);

            //Получаем список жанров в виде строки для последующего парсинга.
            lostfilmSerialModel.ListGenreForParse =
                StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordGenre(),
                    CommonHelper.OpenAngleBracket);

            //Получаем год начала показа сериала.
            lostfilmSerialModel.TvSerialYearStart =
                StringHelper.GetSubstring(detailsPane, LostfilmHelper.GetTvSerialHeaderKeywordYearStart(),
                    CommonHelper.OpenAngleBracket);
        }

        private void ParseSerialOverviewDetails(IHtmlDocument htmlDocumentSerialListDetail,
            LostfilmSerialModel lostfilmSerialModel)
        {
            var serialOverviewElement =
                htmlDocumentSerialListDetail.QuerySelector("#left-pane > div:nth-child(5) > div.details-pane");

            var premiereDateTextElement = serialOverviewElement.QuerySelector(
                "#left-pane > div:nth-child(5) > div.details-pane > div.left-box > a:nth-child(1)");
            lostfilmSerialModel.PremiereDateText = premiereDateTextElement?.InnerHtml;

            var serialOverviewText = serialOverviewElement.InnerHtml;
            string keyword = "Рейтинг IMDb:";

            string rateImDbText = StringHelper.GetSubstring(serialOverviewText, keyword, "<");
            double.TryParse(rateImDbText, out double rateImDb);

            lostfilmSerialModel.RateImDb = rateImDb;

            var officialSiteElement =
                serialOverviewElement.QuerySelector(
                    "#left-pane > div:nth-child(5) > div.details-pane > div.right-box > a:nth-child(8)");
            lostfilmSerialModel.OfficialSite = officialSiteElement?.InnerHtml;
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
                lostfilmSerialModel.NewSeriaDetailNewUrl = seriaDetailNewUrl.Substring(6, indexEnd - indexStart);
            }

            var newSeriaDateReleaseRu =
                serialListDetailElement.QuerySelector("#left-pane > div.text-block.guide > div.body > table > tbody > tr > td.delta");

            var newSeriaDateReleaseText = newSeriaDateReleaseRu.InnerHtml;

            string keyword = "Ru:";
            string dateReleaseRuText = StringHelper.GetSubstring(newSeriaDateReleaseText, keyword, "<");
            DateTime.TryParse(dateReleaseRuText, out DateTime dateReleaseRu);
            lostfilmSerialModel.DateReleaseRu = dateReleaseRu;

            keyword = "Eng:";
            string dateReleaseEnText = StringHelper.GetSubstring(newSeriaDateReleaseText, keyword, "<");
            DateTime.TryParse(dateReleaseEnText, out DateTime dateReleaseEn);
            lostfilmSerialModel.DateReleaseEn = dateReleaseEn;
        }

        private void ParseDetailNewSeria(IHtmlDocument htmlDetailNewSeria, LostfilmSerialModel lostfilmSerialModel)
        {
            var durationInMinElement = htmlDetailNewSeria.QuerySelector(
                "#left-pane > div.white-background.clearfix > div:nth-child(5) > div.details-pane > div:nth-child(2)");
            string durationInMinText = durationInMinElement.InnerHtml;

            string keyword = "Длительность:";
            durationInMinText = StringHelper.GetSubstring(durationInMinText, keyword, "<");
            durationInMinText = Regex.Replace(durationInMinText, @"[а-я]", "");
            double.TryParse(durationInMinText, out double durationInMin);
            lostfilmSerialModel.DurationInMin = durationInMin;
        }
    }
}
