using AngleSharp.Dom.Html;
using Rocket.Parser.Models;

namespace Rocket.Parser.Core.AlbumInfo
{
    /// <summary>
    /// Реализация парсера для парсинга информации о конкретном релизе с сайта album-info.ru
    /// </summary>
    public class AlbumInfoReleaseParser : IParser<AlbumInfoRelease>
    {
        public AlbumInfoRelease Parse(IHtmlDocument document)
        {
            var release = new AlbumInfoRelease
            {
                Name = document.QuerySelector("#dvContent > h1").TextContent,
                Date = document.QuerySelector("#aspnetForm > table > tbody > tr:nth-child(1) > th > div")
                    .TextContent,
                ImageUrl = document.QuerySelector("#aspnetForm > table > tbody > tr:nth-child(1) > th > a")
                    .GetAttribute("href"),
                Genre = document.QuerySelector("#conGenres").TextContent
            };

            return release;
        }
    }
}
