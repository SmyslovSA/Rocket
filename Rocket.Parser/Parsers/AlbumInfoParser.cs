using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.UoW;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;
using AngleSharp.Dom.Html;
using System.Collections.Generic;
using Rocket.DAL.Common.Repositories.Temp;
using Rocket.Parser.Properties;

namespace Rocket.Parser.Parsers
{
    internal class AlbumInfoParser : IAlbumInfoParser
    {
        private readonly ILoadHtmlService _loadHtmlService;
        private readonly IRepository<ParserSettingsEntity> _parserSettingsRepository;
        private readonly IRepository<ResourceEntity> _resourceRepository;
        private readonly IRepository<ResourceItemEntity> _resourceItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loadHtmlService">Сервис загрузки HTML</param>
        /// <param name="parseAlbumInfoService">Сервис парсинга сайта album-info.ru</param>
        /// <param name="parserUoW">UoW для парсера</param>
        public AlbumInfoParser(ILoadHtmlService loadHtmlService,
            IRepository<ParserSettingsEntity> parserSettingsRepository,
            IRepository<ResourceEntity> resourceRepository,
            IRepository<ResourceItemEntity> resourceItemRepository,
            IUnitOfWork unitOfWork)
        {
            _loadHtmlService = loadHtmlService;
            _parserSettingsRepository = parserSettingsRepository;
            _resourceRepository = resourceRepository;
            _resourceItemRepository = resourceItemRepository;
            _unitOfWork = unitOfWork;
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
                var resource = _resourceRepository
                    .Queryable().FirstOrDefault(r => r.Name.Equals(Resources.AlbumInfoSettings));
                var settings = _parserSettingsRepository.Queryable().
                    Where(ps => ps.ResourceId == resource.Id).ToList();

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
                        var releaseLinkList = ParseAlbumlist(linksPageHtmlDoc);

                        //каждый релиз на странице обрабатываем в своем потоке
                        Parallel.ForEach(releaseLinkList, releaseLink =>
                        {
                            var releaseUrl = Resources.AlbumInfoBaseUrl + releaseLink;
                            var resourceInternalId = releaseLink.Replace(
                                Resources.AlbumInfoInternalPrefixId, "");

                            resourceItemsBc.Add(new ResourceItemEntity
                            {
                                ResourceId = setting.ResourceId,
                                ResourceInternalId = resourceInternalId,
                                ResourceItemLink = releaseLink
                            });

                            //парсим страницу релиза
                            var releaseHtmlDoc = _loadHtmlService.GetHtmlDocumentByUrlAsync(releaseUrl);
                            var release = ParseRelease(releaseHtmlDoc);

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
                //находим соответствующий релиз
                var release = releasesBc.FirstOrDefault(r => r.ResourceInternalId == resourceItem.ResourceInternalId);

                if (release != null)
                {
                    var resourceItemEntity = _resourceItemRepository.Queryable().FirstOrDefault(ri =>
                        ri.ResourceId == resourceItem.ResourceId &&
                        ri.ResourceInternalId == resourceItem.ResourceInternalId);

                    if (resourceItemEntity != null)
                    {
                        // обновляем запись если существует
                        resourceItemEntity.ResourceItemLink = resourceItem.ResourceItemLink;
                        _resourceItemRepository.Update(resourceItemEntity);

                        resourceItem.Id = resourceItemEntity.Id;
                    }
                    else
                    {
                        _resourceItemRepository.Insert(resourceItem);
                    }

                    //todo сохраняем релиз
                    
                }
                else
                {
                    //todo пишем в лог что не существует релиза / релиз не распаршен
                }


            }

            _unitOfWork.SaveChanges();

            //очищаем коллекции
            resourceItemsBc = null;
            releasesBc = null;
            
        }

        /// <summary>
        /// Парсинг страницы содержащей ссылки на релизы
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Массив ссылок на страницы релизов</returns>
        private string[] ParseAlbumlist(IHtmlDocument document)
        {
            var list = new List<string>();

            // парсинг таблицы содержащей релизы
            for (int i = 1; i < 4; i++) // столбцы таблицы
            {
                for (int j = 1; j < 5; j++) // строки таблицы
                {
                    var item = document.QuerySelector(
                        String.Format(Resources.AlbumInfoReleaseLinkSelector, i, j));

                    if (item != null)
                    {
                        list.Add(item.GetAttribute(Resources.HrefAttribute));
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
        private AlbumInfoRelease ParseRelease(IHtmlDocument document)
        {
            var release = new AlbumInfoRelease
            {
                Name = document.QuerySelector(Resources.AlbumInfoReleaseNameSelector).TextContent,
                Date = document.QuerySelector(Resources.AlbumInfoReleaseDateSelector).TextContent,
                ImageUrl = document.QuerySelector(Resources.AlbumInfoReleaseImageUrlSelector)
                    .GetAttribute(Resources.HrefAttribute),
                Genre = document.QuerySelector(Resources.AlbumInfoReleaseGenreSelector).TextContent
            };

            return release;
        }

    }
}
