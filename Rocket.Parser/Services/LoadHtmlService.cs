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
        private readonly HttpClient _httpClient;
        private readonly HtmlParser _domParser;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="domParser"></param>
        public LoadHtmlService(HttpClient httpClient, HtmlParser domParser)
        {
            _httpClient = httpClient;
            _domParser = domParser;
        }

        /// <summary>
        /// Получает Html в виде строки по ссылке.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        public async Task<string> GetTextByUrlAsync(string url)
        {
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

            var source = string.Empty;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
            var source = await GetTextByUrlAsync(url).ConfigureAwait(false);

            var htmldocument = await _domParser.ParseAsync(source).ConfigureAwait(false);

            return htmldocument;
        }
    }
}
