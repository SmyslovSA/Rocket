using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace Rocket.Parser.Services
{
    /// <summary>
    /// Proove of concept!
    /// </summary>
    public class ParseLostfilmService
    {
        private string _baseUrl = "http://www.lostfilm.tv";
        private string _serialAddUrl = "/series";

        public async Task<int> Parse()
        {
            try
            {
                var httpClient = new HttpClient();

                var responseSerialList = await httpClient.GetAsync(_baseUrl + _serialAddUrl);

                var htmlDocumentSerialList = await GetHtmlDocumentByResponse(responseSerialList);

                var elSerialList = htmlDocumentSerialList.QuerySelector("#serials_list");
                var elFirstSerial = elSerialList.QuerySelector("#serials_list > div:nth-child(2)");
                var elAdditionalHref = elFirstSerial.QuerySelector("#serials_list > div:nth-child(2) > a");

                var additionalHref = elAdditionalHref.GetAttribute("href");

                var responseFirstSerial = await httpClient.GetAsync(_baseUrl + additionalHref);
                var htmlDocumentFirstSerial = await GetHtmlDocumentByResponse(responseFirstSerial);

                var episodeGuide = htmlDocumentFirstSerial.QuerySelector("#left-pane > div.text-block.guide");
                var newSerialRef = episodeGuide.QuerySelector("#left-pane > div.text-block.guide > div.body > table > tbody > tr.not-available");
                var newSerialRef2 = newSerialRef.QuerySelector("#left-pane > div.text-block.guide > div.body > table > tbody > tr.not-available > td.beta");

                var additionalHref2 = newSerialRef2.GetAttribute("onclick");
                int indexFirst = additionalHref2.IndexOf("'");
                int indexLast = additionalHref2.IndexOf("'", indexFirst + 1) - 1;
                string additionalHref3 = additionalHref2.Substring(6, indexLast - indexFirst);

                var responseDetailNewSerial = await httpClient.GetAsync(_baseUrl + additionalHref3);
                var htmlDetailNewSerial = await GetHtmlDocumentByResponse(responseDetailNewSerial);


                //newSerialRef.

                return 0;

                //if (item != null)
                //{
                //    list.Add(item.GetAttribute("href"));
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //todo а вот это похоже на общий кусочек!
        private async Task<IHtmlDocument> GetHtmlDocumentByResponse(HttpResponseMessage response)
        {
            string source = string.Empty;
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            var domParser = new HtmlParser();

            return await domParser.ParseAsync(source);
        }
    }
}
