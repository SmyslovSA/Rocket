using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Services
{
    /// <summary>
    /// Загружает Html
    /// </summary>
    public class LoadHtmlService : ILoadHtmlService
    {
        readonly HttpClient _client;
        
        public LoadHtmlService()
        {
            _client = new HttpClient();
        }

        /// <summary>
        /// Загружает Html
        /// </summary>
        /// <param name="id">Префикс</param>
        /// /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        public async Task<string> GetSourceById(string id, string url)
        {
            var currentUrl = url.Replace("{CurrentId}", id);
            var response = await _client.GetAsync(currentUrl);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
