using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;

namespace Rocket.Parser.Services
{
    /// <summary>
    /// Парсит сайт album-info.ru
    /// </summary>
    public class ParseAlbumInfoService : IParseAlbumInfoService, IParseService
    {
        #region Fields
        private readonly ILoadHtmlService _loadHtmlService;
        private readonly IRepository<DbParserSettings> _dbParserSettingsRepository;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loadHtmlService">Сервис загрузки HTML</param>
        /// <param name="dbParserSettingsRepository">Репозиторий параметров парсера</param>
        public ParseAlbumInfoService(ILoadHtmlService loadHtmlService,
            IRepository<DbParserSettings> dbParserSettingsRepository)
        {
            _loadHtmlService = loadHtmlService;
            _dbParserSettingsRepository = dbParserSettingsRepository;
        }
        #endregion

        public void Parse()
        {
            // todo опеределить способ получение настроек
            //var settings = _dbParserSettingsRepository.GetByID(1);

            //todo пока настройки получаем так, потом из бд из админки
            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["AlbumInfoParseBaseUrl"];

            var settings = new DbParserSettings
            {
                BaseUrl = baseUrl,
                Prefix = "page={CurrentId}",
                StartPoint = 1,
                EndPoint = 2
            };


            //обрабатываем постранично
            for (int i = settings.StartPoint; i <= settings.EndPoint; i++)
            {
                //загружаем страницу со ссылками на релизы
                string currentUrl = $"{settings.BaseUrl}{settings.Prefix}";
                currentUrl = currentUrl.Replace("{CurrentId}", i.ToString());
                var source = Task.Run(() => _loadHtmlService.GetTextByUrlAsync(currentUrl)).Result;
                var domParser = new HtmlParser();

                var document = Task.Run(() => domParser.ParseAsync(source)).Result;
                //получаем ссылки на страницы релизов
                var releaseLinkList = ParseAlbumlist(document);

                var resourceItems = new List<DbResourceItem>();

                foreach (var releaseLink in releaseLinkList)
                {
                    var releases = new List<AlbumInfoRelease>();

                    //загружаем страницу релиза
                    currentUrl = "http://www.album-info.ru/" + releaseLink;
                    currentUrl = currentUrl.Replace("{CurrentId}", i.ToString());
                    var releaseSource = Task.Run(() => _loadHtmlService.GetTextByUrlAsync(currentUrl)).Result;

                    resourceItems.Add(new DbResourceItem
                    {
                        ResourceId = settings.ResourceId,
                        ResourceInternalId = releaseLink.Replace("albumview.aspx?ID=", ""),
                        ResourceItemLink = releaseLink,
                        CreateDateTime = DateTime.Now
                    });

                    if (releaseSource != null)
                    {
                        //todo падает на этой функции
                        //парсим страницу релиза
                        var release = ParseRelease(document);

                        if (release != null)
                        {
                            releases.Add(release);
                        }
                    }
                }

                //todo сохраняем в БД resourceItems
                // todo сохраняем в БД releases 
            }
        }

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
