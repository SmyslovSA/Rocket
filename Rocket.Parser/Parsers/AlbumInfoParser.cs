using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;

namespace Rocket.Parser.Parsers
{
    public class AlbumInfoParser : IAlbumInfoParser
    {
        private readonly ILoadHtmlService _loadHtmlService;
        private readonly IParseAlbumInfoService _parseAlbumInfoService;
        //private readonly IRepository<DbParserSettings> _dbParserSettingsRepository;
        
        #region Ctor
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
        #endregion
        

        #region Methods
        /// <summary>
        /// Запуск парсинга сайта album-info.ru 
        /// </summary>
        public async Task<int> ParseAsync()
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

                //обрабатываем постранично
                for (int i = settings.StartPoint; i <= settings.EndPoint; i++)
                {
                    //загружаем страницу со ссылками на релизы
                    var source = await _loadHtmlService.GetHtmlByUrlAsync(
                        $"{settings.BaseUrl}{settings.Prefix.Replace("{CurrentId}",i.ToString())}");

                    var domParser = new HtmlParser();
                    var document = await domParser.ParseAsync(source);

                    //получаем ссылки на страницы релизов
                    var releaseLinkList = _parseAlbumInfoService.ParseAlbumlist(document);

                    var resourceItems = new List<DbResourceItem>();

                    foreach (var releaseLink in releaseLinkList)
                    {
                        var releases = new List<AlbumInfoRelease>();

                        //загружаем страницу релиза
                        var releaseSource = await _loadHtmlService.GetHtmlByUrlAsync("http://www.album-info.ru/" + releaseLink);

                        resourceItems.Add(new DbResourceItem
                        {
                            ResourceId = settings.ResourceId,
                            ResourceInternalId = releaseLink.Replace("albumview.aspx?ID=", ""),
                            ResourceItemLink = releaseLink,
                            CreateDateTime = DateTime.Now
                        });

                        if (releaseSource != null)
                        {
                            //парсим страницу релиза
                            var documentRelease = await domParser.ParseAsync(releaseSource);
                            var release = _parseAlbumInfoService.ParseRelease(documentRelease);

                            if (release != null)
                            {
                                releases.Add(release);
                            }
                        }

                    }

                    //todo сохраняем в БД resourceItems
                    // todo сохраняем в БД releases 
                    
                }
                return 1;
            }
            catch (Exception excpt)
            {
                //todo логирование
                throw excpt;
            }
            
            //todo логирование парсер отработал
        }
        #endregion
    }
}
