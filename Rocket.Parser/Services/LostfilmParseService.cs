using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Rocket.Parser.Heplers;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Services
{
    /// <summary>
    /// Proove of concept!
    /// </summary>
    internal class LostfilmParseService : IParseService
    {
        private readonly ILoadHtmlService _loadHtmlService;

        public LostfilmParseService(ILoadHtmlService loadHtmlService)
        {
            _loadHtmlService = loadHtmlService;
        }

        private class LostfilmSerialModel
        {
            /// <summary>
            /// Дополнительная ссылка для получения деталей по сериалу.
            /// </summary>
            public string AddUrlForDetail { get; set; }

            public string ImageUrlTVSerialThumb { get; set; }

            public string TVSerialNameRu { get; set; }

            public string TVSerialNameEn { get; set; }

            public string TVSerialCurrentStatus { get; set; }

            public string TVSerialYearStart { get; set; }

            public string TVSerialCanal { get; set; }

            public string ListGenreForParse { get; set; }

            public double TVSerialRateOnLostFilm { get; set; }
        }

        public void Parse() 
        {
            try
            {
                Task.Run(() => ParseLostFilmAsync());
            }
            catch (Exception e)
            {
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
                var baseUrl = System.Configuration.ConfigurationManager.AppSettings["LostfilmParseBaseUrl"];

                var htmlDocumentSerialList = await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + "/series");

                var elSerialList = htmlDocumentSerialList.QuerySelector("#serials_list");

                var listLostfilmSerialModel = new List<LostfilmSerialModel>();
                int i = 2;
                IElement serialTopHtml;
                do
                {

                    serialTopHtml = elSerialList.QuerySelector($"#serials_list > div:nth-child({i})");
                    if (serialTopHtml == null) break;

                    var lostfilmSerialModel = new LostfilmSerialModel();

                    var addUrlForDetailElement = serialTopHtml.QuerySelector($"#serials_list > div:nth-child({i}) > a");
                    lostfilmSerialModel.AddUrlForDetail = addUrlForDetailElement.GetAttribute("href");

                    var imageUrlTvSerialThumbElement =
                        serialTopHtml.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.picture-box > img.thumb");
                    lostfilmSerialModel.ImageUrlTVSerialThumb = "http:" + imageUrlTvSerialThumbElement.GetAttribute("src");

                    var tvSerialNameRuElement =
                        serialTopHtml.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.body > div.name-ru");
                    lostfilmSerialModel.TVSerialNameRu = tvSerialNameRuElement.InnerHtml;

                    var tvSerialNameEnElement =
                        serialTopHtml.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.body > div.name-en");
                    lostfilmSerialModel.TVSerialNameEn = tvSerialNameEnElement.InnerHtml;

                    var detailsPaneElement = serialTopHtml.QuerySelector($"#serials_list > div:nth-child({i}) > a > div.body > div.details-pane");
                    string detailsPane = detailsPaneElement.InnerHtml;

                    int startIndex = detailsPane.IndexOf(LostFilmSerailListHelper.GetKeywordStatus()) +
                                                         LostFilmSerailListHelper.GetKeywordStatus().Length;
                    int endIndex = detailsPane.IndexOf("<", startIndex);
                    if (endIndex < 0) endIndex = detailsPane.Length;
                    string tvSerialCurrentStatus = detailsPane.Substring(startIndex, endIndex - startIndex);
                    lostfilmSerialModel.TVSerialCurrentStatus = Regex.Replace(tvSerialCurrentStatus, @"[ \t\n\r\f\v]", "");

                    startIndex = detailsPane.IndexOf(LostFilmSerailListHelper.GetKeywordCanal()) +
                                     LostFilmSerailListHelper.GetKeywordCanal().Length;
                    endIndex = detailsPane.IndexOf("<", startIndex);
                    if (endIndex < 0) endIndex = detailsPane.Length;
                    string tvSerialCanal = detailsPane.Substring(startIndex, endIndex - startIndex);
                    lostfilmSerialModel.TVSerialCanal = Regex.Replace(tvSerialCanal, @"[ \t\n\r\f\v]", "");

                    startIndex = detailsPane.IndexOf(LostFilmSerailListHelper.GetKeywordGenre()) +
                                 LostFilmSerailListHelper.GetKeywordGenre().Length;
                    endIndex = detailsPane.IndexOf("<", startIndex);
                    if (endIndex < 0) endIndex = detailsPane.Length;
                    string listGenreForParse = detailsPane.Substring(startIndex, endIndex - startIndex);
                    lostfilmSerialModel.ListGenreForParse = Regex.Replace(listGenreForParse, @"[ \t\n\r\f\v]", "");

                    startIndex = detailsPane.IndexOf(LostFilmSerailListHelper.GetKeywordYearStart()) +
                                 LostFilmSerailListHelper.GetKeywordYearStart().Length;
                    endIndex = detailsPane.IndexOf("<", startIndex);
                    if (endIndex < 0) endIndex = detailsPane.Length;
                    string tvSerialYearStart = detailsPane.Substring(startIndex, endIndex - startIndex);
                    lostfilmSerialModel.TVSerialYearStart = Regex.Replace(tvSerialYearStart, @"[ \t\n\r\f\v]", "");

                    var lostfilmRateElement = 
                        serialTopHtml.QuerySelector($"#serials_list > div:nth-child({i}) > div.mark-green-box");

                    double.TryParse(lostfilmRateElement.InnerHtml, out double lostfilmRate);
                    lostfilmSerialModel.TVSerialRateOnLostFilm = lostfilmRate;

                    listLostfilmSerialModel.Add(lostfilmSerialModel);

                    i = i + 2;
                }
                while (serialTopHtml != null);

                //    < a href = "/series/Cloak_and_Dagger" class="no-decoration">
                //    <div class="picture-box">
                //    <img src = "//static.lostfilm.tv/Images/368/Posters/image.jpg" class="thumb">
                //    <img src = "/vision/thumb-shadow.png" class="thumb-shadow">
                //    </div>
                //    <div class="body">
                //    <div class="name-ru">Плащ и Кинжал</div>
                //    <div class="clr"></div>
                //    <div class="name-en">Cloak and Dagger</div>
                //    <div class="clr"></div>
                //    <div class="details-pane">
                //    Статус: 
                //Премьера
                //    <br>
                //    Год выхода: 2018<br>
                //    Канал: Freeform<br>
                //    Жанр: Боевик, Комиксы, Криминал, Фантастика
                //    </div>
                //    </div>
                //    </a>
                //    <div class="mark-green-box" title="Оценка сериала пользователями Lostfilm.TV">0</div>
                //    <div class="subscribe-box" id="fav_368" title="Добавить сериал в избранное" onclick="FollowSerial(368)"></div>




                var elFirstSerial = elSerialList.QuerySelector("#serials_list > div:nth-child(2)");
                var elAdditionalHref = elFirstSerial.QuerySelector("#serials_list > div:nth-child(2) > a");

                var additionalHref = elAdditionalHref.GetAttribute("href");

                var htmlDocumentFirstSerial = await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + additionalHref);

                var episodeGuide = htmlDocumentFirstSerial.QuerySelector("#left-pane > div.text-block.guide");
                var newSerialRef = episodeGuide.QuerySelector("#left-pane > div.text-block.guide > div.body > table > tbody > tr.not-available");
                var newSerialRef2 = newSerialRef.QuerySelector("#left-pane > div.text-block.guide > div.body > table > tbody > tr.not-available > td.beta");

                var additionalHref2 = newSerialRef2.GetAttribute("onclick");
                int indexFirst = additionalHref2.IndexOf("'");
                int indexLast = additionalHref2.IndexOf("'", indexFirst + 1) - 1;
                string additionalHref3 = additionalHref2.Substring(6, indexLast - indexFirst);

                var htmlDetailNewSerial = await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + additionalHref3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

        }
    }
}
