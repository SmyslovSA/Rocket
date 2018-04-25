using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Services
{
    /// <summary>
    /// Proove of concept!
    /// </summary>
    internal class ParseLostfilmService : IParseService
    {
        private readonly ILoadHtmlService _loadHtmlService;

        private string _serialAddUrl = "/series";

        public ParseLostfilmService(ILoadHtmlService loadHtmlService)
        {
            _loadHtmlService = loadHtmlService;
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

            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["LostfilmParseBaseUrl"];

            var htmlDocumentSerialList = await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + _serialAddUrl);

            var elSerialList = htmlDocumentSerialList.QuerySelector("#serials_list");
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
    }
}
