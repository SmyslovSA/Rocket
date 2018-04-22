using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rocket.Parser.Core
{
    /// <summary>
    /// Загрузчик HTML
    /// </summary>
    class HtmlLoader
    {
        readonly HttpClient _client;
        readonly string _url;

        public HtmlLoader(IParserSettings settings)
        {
            _client = new HttpClient();
            _url = $"{settings.BaseUrl}{settings.Prefix}";
        }

        /// <summary>
        /// Загружает Html
        /// </summary>
        /// <param name="id">Префикс</param>
        /// <returns>Html в виде строки</returns>
        public async Task<string> GetSourceById(string id)
        {
            var currentUrl = _url.Replace("{CurrentId}", id);
            var response = await _client.GetAsync(currentUrl);
            string source = null;

            if(response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
