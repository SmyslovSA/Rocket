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
        /// <summary>
        /// Получает Html в виде строки по ссылке.
        /// </summary>
        /// <exception cref = "NotGetTextByUrlException" >"Не удалось загрузить текст по ссылке {0}."</exception >
        /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        public string GetTextByUrl(string url)
        {
            try
            {
                HttpResponseMessage response;
                using (var httpClient = new HttpClient())
                {
                    response = Task.Run(() => httpClient.GetAsync(url)).Result;
                }

                string source = string.Empty;

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = Task.Run(() => response.Content.ReadAsStringAsync()).Result;
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
        public IHtmlDocument GetHtmlDocumentByUrl(string url)
        {
            try
            {
                var source = GetTextByUrl(url);

                var domParser = new HtmlParser();

                var htmldocument = domParser.Parse(source);

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
