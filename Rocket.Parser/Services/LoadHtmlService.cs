using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Rocket.Parser.Exceptions;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Services
{
    /// <summary>
    /// Сервис для загрузки html
    /// </summary>
    public class LoadHtmlService : ILoadHtmlService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpClient"></param>
        public LoadHtmlService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Получает Html в виде строки по ссылке.
        /// </summary>
        /// <exception cref = "NotGetTextByUrlException" >"Не удалось загрузить текст по ссылке {0}."</exception >
        /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        public string GetTextByUrlAsync(string url) //todo rename async
        {
            try
            {
                var response = _httpClient.GetAsync(url).Result;

                var source = string.Empty;

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source =  response.Content.ReadAsStringAsync().Result;
                }

                return source;
            }
            catch (Exception e)
            {
                throw new NotGetTextByUrlException(url, e);
            }
        }

        /// <summary>
        /// Получает Html по ссылке.
        /// </summary>
        /// <exception cref = "NotGetHtmlDocumentByUrlException">"Не удалось загрузить HtmlDocument по ссылке {0}."</exception >
        /// <param name="url">URL</param>
        /// <returns>HtmlDocument</returns>
        public IHtmlDocument GetHtmlDocumentByUrlAsync(string url)
        {
            try
            {
                var source = GetTextByUrlAsync(url);

                var domParser = new HtmlParser();

                var htmldocument = domParser.ParseAsync(source).Result;

                return htmldocument;
            }
            catch (NotGetTextByUrlException notGetTextByUrlException)
            {
                throw new NotGetHtmlDocumentByUrlException(url, notGetTextByUrlException.InnerException);
            }
            catch (Exception e)
            {
                throw new NotGetHtmlDocumentByUrlException(url, e);
            }
        }
    }
}
