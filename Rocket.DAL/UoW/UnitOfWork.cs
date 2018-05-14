using System;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Context;

namespace Rocket.DAL.UoW
{
    public class UnitOfWork: IUnitOfWork
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
        public UnitOfWork(RocketContext rocketContext,
            IRepository<DbMusic> musicRepository,
            IRepository<ParserSettingsEntity> parserSettingsRepository,
            IRepository<ResourceEntity> resourceRepository,
            IRepository<ResourceItemEntity> resourceItemRepository,
            IRepository<DbMusicGenre> musicGenreRepository,
            IRepository<DbMusicTrack> musicTrackRepository,
            IRepository<DbMusician> musicianRepository
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
        public IRepository<DbMusic> MusicRepository { get; }

        /// <summary>
        /// Репозиторий настроек парсера
        /// </summary>
        public IRepository<ParserSettingsEntity> ParserSettingsRepository { get; }

        /// <summary>
        /// Репозиторий ресурса
        /// </summary>
        public IRepository<ResourceEntity> ResourceRepository { get; }

        /// <summary>
        /// Репозиторий элемента ресурса
        /// </summary>
        public IRepository<ResourceItemEntity> ResourceItemRepository { get; }

        /// <summary>
        /// Репозиторий музыкального жанра
        /// </summary>
        public IRepository<DbMusicGenre> MusicGenreRepository { get; }

        /// <summary>
        /// Репозиторий музыкального трека
        /// </summary>
        public IRepository<DbMusicTrack> MusicTrackRepository { get; }

        /// <summary>
        /// Репозиторий музыканта
        /// </summary>
        public IRepository<DbMusician> MusicianRepository { get; }

        public IRepository<GenreEntity> GenreRepository { get; }


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
