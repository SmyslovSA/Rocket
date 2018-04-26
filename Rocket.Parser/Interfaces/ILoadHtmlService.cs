using AngleSharp.Dom.Html;
using System.Threading.Tasks;

namespace Rocket.Parser.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для загрузки html
    /// </summary>
    public interface ILoadHtmlService
    {
        /// <summary>
        /// Получает Html в виде строки по ссылке.
        /// </summary>
        /// /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        Task<string> GetHtmlByUrlAsync(string url);

        /// <summary>
        /// Получает Html по ссылке.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>HtmlDocument</returns>
        Task<IHtmlDocument> GetHtmlDocumentByUrlAsync(string url);
    }
}