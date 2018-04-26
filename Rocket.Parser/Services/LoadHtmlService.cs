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
        readonly HttpClient _client;

        /// <summary>
        /// Ctor
        /// </summary>
        public LoadHtmlService()
        {
            _client = new HttpClient();
        }
        
        /// <summary>
        /// Получает Html в виде строки по ссылке.
        /// </summary>
        /// /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        public async Task<string> GetHtmlByUrlAsync(string url)
        {
            var response = await _client.GetAsync(url);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }

        //todo убрать отсюда
        /// <summary>
        /// Получает Html по ссылке.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>HtmlDocument</returns>
        public async Task<IHtmlDocument> GetHtmlDocumentByUrlAsync(string url)
        {
            var source = await GetHtmlByUrlAsync(url);

            var domParser = new HtmlParser();

            var htmldocument = await domParser.ParseAsync(source);

            return htmldocument;
        }
    }
}
