using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
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
        /// /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        public async Task<string> GetTextByUrlAsync(string url)
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                response = await httpClient.GetAsync(url);
            }

            string source = string.Empty;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }

        /// <summary>
        /// Получает Html по ссылке.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>HtmlDocument</returns>
        public async Task<IHtmlDocument> GetHtmlDocumentByUrlAsync(string url)
        {
            var source = await GetTextByUrlAsync(url);

            var domParser = new HtmlParser();

            var htmldocument = await domParser.ParseAsync(source);

            return htmldocument;
        }
    }
}
