using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.UoW;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;

namespace Rocket.Parser.Parsers
{
    internal class AlbumInfoParser : IAlbumInfoParser
    {
        private readonly ILoadHtmlService _loadHtmlService;
        private readonly IParseAlbumInfoService _parseAlbumInfoService;
        private readonly IParserUoW _parserUoW;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loadHtmlService">Сервис загрузки HTML</param>
        /// <param name="parseAlbumInfoService">Сервис парсинга сайта album-info.ru</param>
        /// <param name="dbParserSettingsRepository">Репозиторий параметров парсера</param>
        public AlbumInfoParser(ILoadHtmlService loadHtmlService,
            IParseAlbumInfoService parseAlbumInfoService, IParserUoW parserUoW)
        {
            _loadHtmlService = loadHtmlService;
            _parseAlbumInfoService = parseAlbumInfoService;
            _parserUoW = parserUoW;
        }

        /// <inheritdoc />
        /// <summary>
        /// Запуск парсинга сайта album-info.ru 
        /// </summary>
        public void Parse()
        {
            //todo логирование парсер запущен
            try
            {
                // todo опеределить способ получение настроек
                //var settings = _dbParserSettingsRepository.GetByID(1);

                var settings = new ParserSettingsEntity
                {
                    BaseUrl = "http://www.album-info.ru/albumlist.aspx?",
                    Prefix = "page={CurrentId}",
                    StartPoint = 2,
                    EndPoint = 2,
                    ResourceId = 1
                };

                var resourceItemsBc = new BlockingCollection<ResourceItemEntity>();
                var releasesBc = new BlockingCollection<AlbumInfoRelease>();

                //обрабатываем постранично (на каждую страницу свой поток)
                Parallel.For(settings.StartPoint, settings.EndPoint + 1, index =>
                {
                    var linksPageUrl = $"{settings.BaseUrl}{settings.Prefix.Replace("{CurrentId}", index.ToString())}";

                    //загружаем страницу со ссылками на релизы
                    var linksPageHtmlDoc = _loadHtmlService.GetHtmlDocumentByUrlAsync(linksPageUrl).Result;

                    //получаем ссылки на страницы релизов
                    var releaseLinkList = _parseAlbumInfoService.ParseAlbumlist(linksPageHtmlDoc);

                    //каждый релиз на странице обрабатываем в своем потоке
                    Parallel.ForEach(releaseLinkList, releaseLink =>
                    {
                        var releaseUrl = "http://www.album-info.ru/" + releaseLink;

                        resourceItemsBc.Add(new ResourceItemEntity
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
                            releasesBc.Add(release);
                        }
                    });
                });

                if (resourceItemsBc.Any())
                {
                    //todo сохранение в БД
                }

                if (releasesBc.Any())
                {
                    //todo сохранение в БД
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
