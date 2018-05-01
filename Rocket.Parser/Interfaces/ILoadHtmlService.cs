using AngleSharp.Dom.Html;
using System.Threading.Tasks;
using Rocket.Parser.Exceptions;

namespace Rocket.Parser.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для загрузки html
    /// </summary>
    internal interface ILoadHtmlService
    {
        /// <summary>
        /// Получает Html в виде строки по ссылке.
        /// </summary>
        /// <exception cref = "NotGetTextByUrlException" >"Не удалось загрузить текст по ссылке {0}."</exception >
        /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        string GetTextByUrlAsync(string url);

        /// <summary>
        /// Получает Html по ссылке.
        /// </summary>
        /// <exception cref = "NotGetHtmlDocumentByUrlException" >"Не удалось загрузить HtmlDocument по ссылке {0}."</exception >
        /// <param name="url">URL</param>
        /// <returns>HtmlDocument</returns>
        IHtmlDocument GetHtmlDocumentByUrlAsync(string url);
    }
}