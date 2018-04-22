using System;
using System.Collections.Generic;
using Rocket.Parser.Core;
using Rocket.Parser.Core.AlbumInfo;
using Rocket.Parser.Models;

namespace Rocket.Parser
{
    /// <summary>
    /// Промежуточный класс для запуска парсера
    /// </summary>
    public class Middleware
    {
        readonly ParserWorker<string[]> _parserItem;
        readonly ParserWorker<AlbumInfoRelease> _parserRelease;

        public Middleware()
        {
            // Элементы ресурса (ID релизов)
            _parserItem = new ParserWorker<string[]>(new AlbumInfoItemParser());
            _parserItem.OnCompleted += Parser_OnCompletedItem;
            _parserItem.OnNewData += Parser_OnNewItemData;

            // релизы
            _parserRelease = new ParserWorker<AlbumInfoRelease>(new AlbumInfoReleaseParser());
            _parserRelease.OnCompleted += Parser_OnCompletedRelease;
            _parserRelease.OnNewData += Parser_OnNewReleaseData;

        }

        private void Parser_OnNewItemData(object arg1, string[] arg2)
        {
            Console.WriteLine(arg2);

            // TODO сохранение в БД через модель ResourceItem

            var resourceItems = new List<ResourceItem>();

            foreach (var item in arg2)
            {
                resourceItems.Add(new ResourceItem
                {
                    ResourceTypeId = 1,
                    ResourceId = item.Replace("albumview.aspx?ID=", ""),
                    ResourceItemLink = item,
                    CreateDateTime = DateTime.Now
                });
            }

            //todo перенести в Parser_OnCompletedItem
            _parserRelease.Start(new AlbumInfoReleaseSettings(1, 1,
                "http://www.album-info.ru/", "{CurrentId}"), resourceItems);

        }

        private void Parser_OnNewReleaseData(object arg1, AlbumInfoRelease arg2)
        {
            Console.WriteLine($"Name:{arg2.Name} - Date:{arg2.Date} - ImageUrl:{arg2.ImageUrl} - Genre:{arg2.Genre}");
        }

        private void Parser_OnCompletedRelease(object obj)
        {
            Console.WriteLine("All releases was load");
        }

        private void Parser_OnCompletedItem(object obj)
        {
            Console.WriteLine("All items was load");
        }

        public void Start(int startPage, int endPage)
        {
            _parserItem.Settings = new AlbumInfoItemSettings(startPage, endPage,
                "http://www.album-info.ru/albumlist.aspx?", "page={CurrentId}");
            _parserItem.Start();
        }

        public void Abort()
        {
            _parserItem.Abort();
            _parserRelease.Abort();
        }

    }
}
