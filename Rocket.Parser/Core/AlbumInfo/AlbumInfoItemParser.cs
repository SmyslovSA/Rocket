using System.Collections.Generic;
using AngleSharp.Dom.Html;

namespace Rocket.Parser.Core.AlbumInfo
{
    /// <summary>
    /// Реализация парсера для парсинга информации о наличии релизов с сайта album-info.ru
    /// </summary>
    public class AlbumInfoItemParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list  = new List<string>();

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
    }
}
