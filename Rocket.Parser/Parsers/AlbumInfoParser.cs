using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
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
        /// <param name="parserUoW">UoW для парсера</param>
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
                // получаем настроейки парсера
                var settings = _parserUoW.GetParserSettingsByResourceName(Properties.Resources.AlbumInfoSettings);

                // для каждой настройки выполняем парсинг
                foreach (var setting in settings)
                {
                    var resourceItemsBc = new BlockingCollection<ResourceItemEntity>();
                    var releasesBc = new BlockingCollection<AlbumInfoRelease>();
                    
                    //обрабатываем постранично (на каждую страницу свой поток)
                    Parallel.For(setting.StartPoint, setting.EndPoint + 1, index =>
                    {
                        var linksPageUrl = $"{setting.BaseUrl}{setting.Prefix}{index.ToString()}";

                        //загружаем страницу со ссылками на релизы
                        var linksPageHtmlDoc = _loadHtmlService.GetHtmlDocumentByUrlAsync(linksPageUrl);

                        //получаем ссылки на страницы релизов
                        var releaseLinkList = _parseAlbumInfoService.ParseAlbumlist(linksPageHtmlDoc);

                        //каждый релиз на странице обрабатываем в своем потоке
                        Parallel.ForEach(releaseLinkList, releaseLink =>
                        {
                            var releaseUrl = Properties.Resources.AlbumInfoBaseUrl + releaseLink;
                            var resourceInternalId = releaseLink.Replace(
                                Properties.Resources.AlbumInfoInternalPrefixId, "");

                            resourceItemsBc.Add(new ResourceItemEntity
                            {
                                ResourceId = setting.ResourceId,
                                ResourceInternalId = resourceInternalId,
                                ResourceItemLink = releaseLink
                            });

                            //парсим страницу релиза
                            var releaseHtmlDoc = _loadHtmlService.GetHtmlDocumentByUrlAsync(releaseUrl);
                            var release = _parseAlbumInfoService.ParseRelease(releaseHtmlDoc);

                            if (release != null)
                            {
                                release.ResourceInternalId = resourceInternalId;
                                releasesBc.Add(release);
                            }
                        });
                    });

                    //фиксация данных в БД
                    SaveResults(resourceItemsBc, releasesBc);
                }

            }
            catch (Exception excpt)
            {
                //todo логирование
                throw excpt;
            }
            
            //todo логирование парсер отработал
        }

        /// <summary>
        /// Сохраняет в БД список элементов ресурса
        /// </summary>
        /// <returns></returns>
        private void SaveResults(BlockingCollection<ResourceItemEntity> resourceItemsBc,
            BlockingCollection<AlbumInfoRelease> releasesBc)
        {
            if (!resourceItemsBc.Any() && !releasesBc.Any()) throw new NotImplementedException();  //todo

            var resourceItems = resourceItemsBc.ToList();

            foreach (var resourceItem in resourceItems)
            {
                //var resourceInternalId = resourceItem.ResourceInternalId;
                //var param = Expression.Parameter(typeof(ResourceItemEntity), "ri");
                //Expression boby = Expression.Equal(Expression.PropertyOrField(param, "ResourceInternalId"),
                //    Expression.Constant(resourceInternalId, typeof(string)));
                //var filter = Expression.Lambda<Func<ResourceItemEntity, bool>>(boby, param);

                //var resourceItemEntity = _parserUoW.ResourceItems.Get(filter);

                //находим соответствующий релиз
                var release = releasesBc.FirstOrDefault(r => r.ResourceInternalId == resourceItem.ResourceInternalId);

                if (release != null)
                {
                    var resourceItemEntity = _parserUoW.ResourceItems.Get(
                            r => r.ResourceId == resourceItem.ResourceId &&
                                 r.ResourceInternalId == resourceItem.ResourceInternalId).
                        FirstOrDefault();

                    if (resourceItemEntity != null)
                    {
                        // обновляем запись если существует
                        resourceItem.Id = resourceItemEntity.Id;
                        _parserUoW.ResourceItems.Update(resourceItem);
                    }
                    else
                    {
                        _parserUoW.ResourceItems.Insert(resourceItem);
                    }

                    //todo сохраняем релиз



                    _parserUoW.Save();
                }
                else
                {
                    //todo пишем в лог что не существует релиза / релиз не распаршен
                }


            }

            //очищаем коллекции
            resourceItemsBc = null;
            releasesBc = null;

        }
    }
}
