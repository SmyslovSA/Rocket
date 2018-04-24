using AngleSharp.Dom.Html;
using Rocket.Parser.Models;

namespace Rocket.Parser.Interfaces
{
    public interface IParseAlbumInfoService
    {
        /// <summary>
        /// Парсинг страницы содержащей ссылки на релизы
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Массив ссылок на страницы релизов</returns>
        string[] ParseAlbumlist(IHtmlDocument document);

        /// <summary>
        /// Парсинг страницы релиза
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Модель релиза на сайте album-info.ru</returns>
        AlbumInfoRelease ParseRelease(IHtmlDocument document);
    }
}