using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;

namespace Rocket.Parser.Parsers
{
    internal class AlbumInfoParser : IAlbumInfoParser
    {
        private readonly ILoadHtmlService _loadHtmlService;
        private readonly IParseAlbumInfoService _parseAlbumInfoService;
        //private readonly IRepository<DbParserSettings> _dbParserSettingsRepository;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loadHtmlService">Сервис загрузки HTML</param>
        /// <param name="parseAlbumInfoService">Сервис парсинга сайта album-info.ru</param>
        /// <param name="dbParserSettingsRepository">Репозиторий параметров парсера</param>
        public AlbumInfoParser(ILoadHtmlService loadHtmlService,
            IParseAlbumInfoService parseAlbumInfoService
            //,
            /*IRepository<DbParserSettings> dbParserSettingsRepository*/)
        {
            _loadHtmlService = loadHtmlService;
            //_dbParserSettingsRepository = dbParserSettingsRepository;
            _parseAlbumInfoService = parseAlbumInfoService;
        }

        /// <summary>
        /// Запуск парсинга сайта album-info.ru 
        /// </summary>
        public async Task ParseAsync()
        {
            //todo логирование парсер запущен
            try
            {
                // todo опеределить способ получение настроек
                //var settings = _dbParserSettingsRepository.GetByID(1);

                var settings = new DbParserSettings
                {
                    BaseUrl = "http://www.album-info.ru/albumlist.aspx?",
                    Prefix = "page={CurrentId}",
                    StartPoint = 2,
                    EndPoint = 2
                };

                var resourceItems = new List<DbResourceItem>();
                var releases = new List<AlbumInfoRelease>();

                //обрабатываем постранично (на каждую страницу свой поток)
                for (int i = settings.StartPoint; i <= settings.EndPoint; i++)
                {
                    var linksPageUrl = $"{settings.BaseUrl}{settings.Prefix.Replace("{CurrentId}", i.ToString())}";

                    await Task.Run(() =>
                    {
                        //загружаем страницу со ссылками на релизы
                        var linksPageHtmlDoc = _loadHtmlService.GetHtmlDocumentByUrlAsync(linksPageUrl).Result;

                        //получаем ссылки на страницы релизов
                        var releaseLinkList = _parseAlbumInfoService.ParseAlbumlist(linksPageHtmlDoc);

                        foreach (var releaseLink in releaseLinkList)
                        {
                            //каждый релиз в своем потоке
                            Task.Run(() =>
                            {
                                var releaseUrl = "http://www.album-info.ru/" + releaseLink;

                                resourceItems.Add(new DbResourceItem
                                {
                                    ResourceId = settings.ResourceId,
                                    ResourceInternalId = releaseLink.Replace("albumview.aspx?ID=", ""),
                                    ResourceItemLink = releaseLink,
                                    CreateDateTime = DateTime.Now
                                });

                                //парсим страницу релиза
                                var releaseHtmlDoc = _loadHtmlService.GetHtmlDocumentByUrlAsync(releaseUrl).Result;
                                var release = _parseAlbumInfoService.ParseRelease(releaseHtmlDoc);

                                if (release != null)
                                {
                                    release.ResourceItemId = settings.ResourceId;
                                    releases.Add(release);
                                }
                            });
                        }
                    });


                    //todo сохраняем в БД resourceItems
                    // todo сохраняем в БД releases 
                    
                }
            }
            catch (Exception excpt)
            {
                //todo логирование
                throw excpt;
            }
            
            //todo логирование парсер отработал
        }
    }
}
