using System.Collections.Generic;
using AngleSharp.Dom.Html;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;

namespace Rocket.Parser.Services
{
    /// <summary>
    /// Парсит сайт album-info.ru
    /// </summary>
    public class ParseAlbumInfoService : IParseAlbumInfoService
    {
        /// <summary>
        /// Парсинг страницы содержащей ссылки на релизы
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Массив ссылок на страницы релизов</returns>
        public string[] ParseAlbumlist(IHtmlDocument document)
        {
            var list = new List<string>();

            // парсинг таблицы содержащей релизы
            for (int i = 1; i < 4; i++) // столбцы таблицы
            {
                for (int j = 1; j < 5; j++) // строки таблицы
                {
                    var item = document.QuerySelector(
                        $"#ctl00_CPH_conAlbums_conAlbums > tbody > tr:nth-child({i}) > td:nth-child({j}) > a");

                    if (item != null)
                    {
                        list.Add(item.GetAttribute("href"));
                    }
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// Парсинг страницы релиза
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Модель релиза на сайте album-info.ru</returns>
        public AlbumInfoRelease ParseRelease(IHtmlDocument document)
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
