using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using Quartz;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;

namespace Rocket.Parser.Jobs
{   
    /// <summary>
    /// Джоба для парсинга сайта album-info.ru
    /// </summary>
    [DisallowConcurrentExecution]
    internal class ParseAlbumInfoJob : IJob
    {
        #region Fields
        private readonly ILoadHtmlService _loadHtmlService;
        private readonly IParseAlbumInfoService _parseAlbumInfoService;
        private readonly IRepository<DbParserSettings> _dbParserSettingsRepository;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loadHtmlService">Сервис загрузки HTML</param>
        /// <param name="parseAlbumInfoService">Сервис парсинга сайта album-info.ru</param>
        /// <param name="dbParserSettingsRepository">Репозиторий параметров парсера</param>
        public ParseAlbumInfoJob(ILoadHtmlService loadHtmlService,
            IParseAlbumInfoService parseAlbumInfoService,
            IRepository<DbParserSettings> dbParserSettingsRepository)
        {
            _loadHtmlService = loadHtmlService;
            _dbParserSettingsRepository = dbParserSettingsRepository;
            _parseAlbumInfoService = parseAlbumInfoService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Запуск парсинга сайта album-info.ru 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
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
                    StartPoint = 1,
                    EndPoint = 2
                };

                var result = Task.Run(async () =>
                {
                    //обрабатываем постранично
                    for (int i = settings.StartPoint; i <= settings.EndPoint; i++)
                    {
                        //загружаем страницу со ссылками на релизы
                        var source = await _loadHtmlService.GetSourceById(i.ToString(), $"{settings.BaseUrl}{settings.Prefix}");
                        var domParser = new HtmlParser();

                        var document = await domParser.ParseAsync(source);
                        //получаем ссылки на страницы релизов
                        var releaseLinkList = _parseAlbumInfoService.ParseAlbumlist(document);

                        var resourceItems = new List<DbResourceItem>();

                        foreach (var releaseLink in releaseLinkList)
                        {
                            var releases = new List<AlbumInfoRelease>();

                            //загружаем страницу релиза
                            var releaseSource = await _loadHtmlService.GetSourceById(i.ToString(), "http://www.album-info.ru/" + releaseLink);

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
                                var release = _parseAlbumInfoService.ParseRelease(document);

                                if (release != null)
                                {
                                    releases.Add(release);
                                }
                            }

                        }

                        //todo сохраняем в БД resourceItems
                        // todo сохраняем в БД releases 
                    }

                    return 0;
                }).Result;

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
