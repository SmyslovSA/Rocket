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
using System.Data.Entity;
using System.Globalization;
using PCRE;
using Rocket.DAL.Common.DbModels;
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
        private readonly IRepository<DbMusic> _musicRepository;
        private readonly IRepository<DbMusicGenre> _musicGenreRepository;
        private readonly IRepository<DbMusicTrack> _musicTrackRepository;
        private readonly IRepository<DbMusician> _musicianRepository;   
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
            IRepository<DbMusic> musicRepository,
            IRepository<DbMusicGenre> musicGenreRepository,
            IRepository<DbMusicTrack> musicTrackRepository,
            IRepository<DbMusician> musicianRepository,
            IUnitOfWork unitOfWork)
        {
            _loadHtmlService = loadHtmlService;
            _parserSettingsRepository = parserSettingsRepository;
            _resourceRepository = resourceRepository;
            _resourceItemRepository = resourceItemRepository;
            _musicRepository = musicRepository;
            _musicGenreRepository = musicGenreRepository;
            _musicTrackRepository = musicTrackRepository;
            _musicianRepository = musicianRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        /// <summary>
        /// Запуск парсинга сайта album-info.ru 
        /// </summary>
        public async Task ParseAsync()
        {
            //todo логирование парсер запущен
            try
            {
                // получаем настройки парсера
                var resource = _resourceRepository
                    .Queryable().FirstOrDefault(r => r.Name.Equals(Resources.AlbumInfoSettings));
                var settings = _parserSettingsRepository.Queryable().
                    Where(ps => ps.ResourceId == resource.Id).ToList();

                // для каждой настройки выполняем парсинг
                foreach (var setting in settings)
                {
                    var resourceItemsBc = new BlockingCollection<ResourceItemEntity>();
                    var releasesBc = new BlockingCollection<AlbumInfoRelease>();

                    var taskList = new List<Task>();

                    for (int index = setting.StartPoint; index <= setting.EndPoint; index++)
                    {
                        var task = ParseAlbumInfo(setting, index, resourceItemsBc, releasesBc);

                        taskList.Add(task);
                    }

                    await Task.WhenAll(taskList.ToArray());

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

        private async Task ParseAlbumInfo(ParserSettingsEntity setting, int index, 
            BlockingCollection<ResourceItemEntity> resourceItemsBc, BlockingCollection<AlbumInfoRelease> releasesBc)
        {
            var linksPageUrl = $"{setting.BaseUrl}{setting.Prefix}{index}";

            //загружаем страницу со ссылками на релизы
            var linksPageHtmlDoc = await _loadHtmlService.GetHtmlDocumentByUrlAsync(linksPageUrl).
                ConfigureAwait(false);

            //получаем ссылки на страницы релизов
            var releaseLinkList = ParseAlbumlist(linksPageHtmlDoc);

            var taskList = new List<Task>();

            foreach (var releaseLink in releaseLinkList)
            {
                var task = ParseReleasInfo(setting, releaseLink, resourceItemsBc, releasesBc);

                taskList.Add(task);
            }

            await Task.WhenAll(taskList.ToArray()).ConfigureAwait(false);

        }

        private async Task ParseReleasInfo(ParserSettingsEntity setting, string releaseLink, 
            BlockingCollection<ResourceItemEntity> resourceItemsBc, BlockingCollection<AlbumInfoRelease> releasesBc)
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
            var releaseHtmlDoc = await _loadHtmlService.GetHtmlDocumentByUrlAsync(releaseUrl).ConfigureAwait(false);
            var release = ParseRelease(releaseHtmlDoc);

            if (release != null)
            {
                release.ResourceInternalId = resourceInternalId;
                releasesBc.Add(release);
            }

        }

        /// <summary>
        /// Сохраняет в БД результаты парсинга
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
                    int musicId;
                    var music = MapAlbumInfoReleaseToMusic(release);

                    //проверяем существует ли релиз
                    var musicEntity = _musicRepository.Queryable().
                        Include(a => a.Genres).
                        Include(m => m.Musicians).
                        Include(t => t.MusicTracks).
                        FirstOrDefault(mr => mr.Title.Equals(music.Title) && mr.Artist.Equals(music.Artist));

                    if (musicEntity != null)
                    {
                        musicEntity.Genres = music.Genres;
                        musicEntity.Musicians = music.Musicians;
                        musicEntity.ReleaseDate = music.ReleaseDate;
                        musicEntity.Duration = music.Duration;
                        musicEntity.MusicTracks = music.MusicTracks;
                        musicEntity.Type = music.Type;

                        _musicRepository.Update(musicEntity);
                        _unitOfWork.SaveChanges();
                        //todo musicEntity.PosterImagePath
                        musicId = musicEntity.Id;
                    }
                    else
                    {
                        //todo musicEntity.PosterImagePath
                        _musicRepository.Insert(music);
                        _unitOfWork.SaveChanges();
                        musicId = music.Id;
                    }

                    resourceItem.MusicId = musicId;

                    var resourceItemEntity = _resourceItemRepository.Queryable().FirstOrDefault(ri =>
                        ri.ResourceId == resourceItem.ResourceId &&
                        ri.ResourceInternalId == resourceItem.ResourceInternalId);

                    if (resourceItemEntity != null)
                    { // обновляем информацию о релизе
                        resourceItemEntity.ResourceItemLink = resourceItem.ResourceItemLink;
                        resourceItemEntity.MusicId = resourceItem.MusicId;

                        _resourceItemRepository.Update(resourceItemEntity);
                        _unitOfWork.SaveChanges();
                        resourceItem.Id = resourceItemEntity.Id;
                    }
                    else
                    { // сохраняем информацию о релизе
                        _resourceItemRepository.Insert(resourceItem);
                        _unitOfWork.SaveChanges();
                    }
                }
                else
                {
                    //todo пишем в лог что не существует релиза / релиз не распаршен
                }
            }
        }

        private DbMusic MapAlbumInfoReleaseToMusic(AlbumInfoRelease release)
        {
            var music = new DbMusic();

            //const string trackSeparator = "<br>";

            const string releaseNamePattern = @"(?<=\- )(?: ?+[^\(])++";
            const string releaseTypePattern = @"(?<=\()\w++";
            const string releaseGenrePattern = @"\w[^,]*+";
            const string releaseTrackListPattern = @"\w(\d*+[^\.])++"; 
            var releaseName = PcreRegex.Match(release.Name, releaseNamePattern).Value;
            var releaseType = PcreRegex.Matches(release.Name, releaseTypePattern)
                .Select(m => m.Value).LastOrDefault();
            var releaseArtist = release.Name.Substring(0, release.Name.IndexOf("-", StringComparison.Ordinal) - 1);
            var releaseGenres = PcreRegex.Matches(release.Genre, releaseGenrePattern).Select(m => m.Value);

            var format = release.Date.Length > 4 ? "d MMMM yyyy г." : "yyyy";
            
            var provider = CultureInfo.CreateSpecificCulture("ru-RU");
            var releaseDate = DateTime.ParseExact(release.Date, format, provider);

            var trackList = PcreRegex.Matches(release.TrackList, releaseTrackListPattern)
                .Select(m => m.Value).ToList();

            //обрабатываем треклист
            foreach (var track in trackList)
            {
                var trackEntity = _musicTrackRepository.Queryable().FirstOrDefault(t => t.Title.Equals(track));
                if (trackEntity != null)
                {
                    music.MusicTracks.Add(trackEntity);
                }
                music.MusicTracks.Add( new DbMusicTrack
                {
                    Title = track
                });
            }

            //проверяем существует ли исполнитель
            var musicianEntity = _musicianRepository.Queryable().
                FirstOrDefault(m => m.FullName.Equals(releaseArtist));
            if (musicianEntity != null)
            {
                music.Musicians.Add(musicianEntity);
            }
            else
            {
                var newMusician = new DbMusician
                {
                    FullName = releaseArtist
                };

                _musicianRepository.Insert(newMusician);
                _unitOfWork.SaveChanges();

                music.Musicians.Add(newMusician);
            }
            

            //проверяем существует ли жанры
            foreach (var releaseGenre in releaseGenres)
            {
                var genreEntity = _musicGenreRepository.Queryable().FirstOrDefault(g => g.Name.Equals(releaseGenre));
                if (genreEntity != null)
                {
                    music.Genres.Add(genreEntity);
                }
                else
                {
                    var newMusicGenres = new DbMusicGenre
                    {
                        Name = releaseGenre
                    };

                    _musicGenreRepository.Insert(newMusicGenres);
                    _unitOfWork.SaveChanges();

                    music.Genres.Add(newMusicGenres);
                }
            }

            music.Title = releaseName;
            music.ReleaseDate = releaseDate;
            music.Type = releaseType;
            music.Artist = releaseArtist;

            return music;
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
                Genre = document.QuerySelector(Resources.AlbumInfoReleaseGenreSelector).TextContent,
                TrackList = document.QuerySelector(Resources.AlbumInfoReleaseTrackListSelector).InnerHtml
            };

            return release;
        }

    }
}
