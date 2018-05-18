using System;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Context;

namespace Rocket.DAL.UoW
{
    public class UnitOfWork : IUnitOfWorkP
    {
        private RocketContext _rocketContext;
        private bool _disposed;

        /// <summary>
        /// Unit of Work для RocketConext
        /// </summary>
        /// <param name="rocketContext">Контекст данных</param>
        /// <param name="parserSettingsRepository">Репозиторий настроек парсера</param>
        /// <param name="resourceRepository">Репозиторий ресурса</param>
        /// <param name="resourceItemRepository">Репозиторий элемента ресурса</param>
        /// <param name="musicRepository">Репозиторий релиза</param>
        /// <param name="musicGenreRepository">Репозиторий жанра</param>
        /// <param name="musicTrackRepository">Репозиторий трека</param>
        /// <param name="musicianRepository">Репозиторий исполнителя</param>
        /// <param name="genreRepository"></param>
        public UnitOfWork(RocketContext rocketContext,
            IBaseRepository<DbMusic> musicRepository,
            IBaseRepository<ParserSettingsEntity> parserSettingsRepository,
            IBaseRepository<ResourceEntity> resourceRepository,
            IBaseRepository<ResourceItemEntity> resourceItemRepository,
            IBaseRepository<DbMusicGenre> musicGenreRepository,
            IBaseRepository<DbMusicTrack> musicTrackRepository,
            IBaseRepository<DbMusician> musicianRepository,
            IBaseRepository<CategoryEntity> categoryRepository,
            IBaseRepository<EpisodeEntity> episodeRepository,
            IBaseRepository<GenreEntity> genreRepository,
            IBaseRepository<PersonEntity> personRepository,
            IBaseRepository<PersonTypeEntity> personTypeRepository,
            IBaseRepository<SeasonEntity> seasonRepository,
            IBaseRepository<TvSeriasEntity> tvSeriasRepository
            )
        {
            _rocketContext = rocketContext;
            MusicRepository = musicRepository;
            ParserSettingsRepository = parserSettingsRepository;
            ResourceRepository = resourceRepository;
            ResourceItemRepository = resourceItemRepository;
            MusicGenreRepository = musicGenreRepository;
            MusicTrackRepository = musicTrackRepository;
            MusicianRepository = musicianRepository;
            CategoryRepository = categoryRepository;
            EpisodeRepository = episodeRepository;
            GenreRepository = genreRepository;
            PersonRepository = personRepository;
            PersonTypeRepository = personTypeRepository;
            SeasonRepository = seasonRepository;
            TvSeriasRepository = tvSeriasRepository;
        }

        /// <summary>
        /// Возвращает репозиторий для фильмов
        /// </summary>
        public IDbFilmRepository FilmRepository => throw new NotImplementedException();

        /// <summary>
        /// Возвращает репозиторий для сериалов
        /// </summary>
        public IDbTVSeriesRepository TVSeriesRepository => throw new NotImplementedException();

        /// <summary>
        /// Возвращает репозиторий для музыкального релиза
        /// </summary>
        public IBaseRepository<DbMusic> MusicRepository { get; }

        /// <summary>
        /// Репозиторий настроек парсера
        /// </summary>
        public IBaseRepository<ParserSettingsEntity> ParserSettingsRepository { get; }

        /// <summary>
        /// Репозиторий ресурса
        /// </summary>
        public IBaseRepository<ResourceEntity> ResourceRepository { get; }

        /// <summary>
        /// Репозиторий элемента ресурса
        /// </summary>
        public IBaseRepository<ResourceItemEntity> ResourceItemRepository { get; }

        /// <summary>
        /// Репозиторий музыкального жанра
        /// </summary>
        public IBaseRepository<DbMusicGenre> MusicGenreRepository { get; }

        /// <summary>
        /// Репозиторий музыкального трека
        /// </summary>
        public IBaseRepository<DbMusicTrack> MusicTrackRepository { get; }

        /// <summary>
        /// Репозиторий музыканта
        /// </summary>
        public IBaseRepository<DbMusician> MusicianRepository { get; }

        public IBaseRepository<CategoryEntity> CategoryRepository { get; }

        public IBaseRepository<EpisodeEntity> EpisodeRepository { get; }

        /// <summary>
        /// Репозиторий жанра
        /// </summary>
        public IBaseRepository<GenreEntity> GenreRepository { get; }

        public IBaseRepository<PersonEntity> PersonRepository { get; }

        public IBaseRepository<PersonTypeEntity> PersonTypeRepository { get; }

        public IBaseRepository<SeasonEntity> SeasonRepository { get; }

        public IBaseRepository<TvSeriasEntity> TvSeriasRepository { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_rocketContext != null)
                {
                    _rocketContext.Dispose();
                    _rocketContext = null;
                }
            }

            _disposed = true;
        }

        public int SaveChanges()
        {
            return _rocketContext.SaveChanges();
        }
    }
}
