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
        public async Task<string> GetTextByUrlAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                var source = string.Empty;

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
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
        public async Task<IHtmlDocument> GetHtmlDocumentByUrlAsync(string url)
        {
            try
            {
                var source = await GetTextByUrlAsync(url);

                var domParser = new HtmlParser();

                var htmldocument = await domParser.ParseAsync(source);

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
