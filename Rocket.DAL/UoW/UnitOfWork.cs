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
    /// <summary>
    /// Представляет unit of work.
    /// Содержит репозитории использующие один контекст данных.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private RocketContext _rocketContext;
        private bool _disposed;

        /// <summary>
        /// Возвращает репозиторий для релизов.
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
            IRepository<DbMusic> musicRepository,
            IRepository<ParserSettingsEntity> parserSettingsRepository,
            IRepository<ResourceEntity> resourceRepository,
            IRepository<ResourceItemEntity> resourceItemRepository,
            IRepository<DbMusicGenre> musicGenreRepository,
            IRepository<DbMusicTrack> musicTrackRepository,
            IRepository<DbMusician> musicianRepository,
            IRepository<CategoryEntity> categoryRepository,
            IRepository<EpisodeEntity> episodeRepository,
            IRepository<GenreEntity> genreRepository,
            IRepository<PersonEntity> personRepository,
            IRepository<PersonTypeEntity> personTypeRepository,
            IRepository<SeasonEntity> seasonRepository,
            IRepository<TvSeriasEntity> tvSeriasRepository
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
        /// Возвращает репозиторий для фильмов.
        /// </summary>
        public IDbFilmRepository FilmRepository => throw new NotImplementedException();

        /// <summary>
        /// Возвращает репозиторий для сериалов.
        /// </summary>
        public IDbTVSeriesRepository TVSeriesRepository => throw new NotImplementedException();

        /// <summary>
        /// Возвращает репозиторий для музыки.
        /// </summary>
        public IRepository<DbMusic> MusicRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для emails.
        /// </summary>
        public IRepository<ParserSettingsEntity> ParserSettingsRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для genre.
        /// </summary>
        public IRepository<ResourceEntity> ResourceRepository { get; }

        /// <summary>
        /// Репозиторий для работы с пользователями.
        /// </summary>
        public IRepository<ResourceItemEntity> ResourceItemRepository { get; }

        /// <summary>
        /// Репозиторий для работы с ролями.
        /// </summary>
        public IRepository<DbMusicGenre> MusicGenreRepository { get; }

        /// <summary>
        /// Репозиторий для работы с пермишенами.
        /// </summary>
        public IRepository<DbMusicTrack> MusicTrackRepository { get; }

        /// <summary>
        /// Репозиотрий для работы с пользователями личного кабинета.
        /// </summary>
        public IRepository<DbMusician> MusicianRepository { get; }

        public IRepository<CategoryEntity> CategoryRepository { get; }

        /// <summary>
        /// /// <summary>
        /// Создает новый экземпляр <see cref="UnitOfWork"/>
        /// c заданным контекстом данных.
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста данных.</param>
        /// <param name="dbReleaseRepository">Экземпляр репозитория релизов.</param>
        /// <param name="dbFilmRepository">Экземпляр репозитория фильмов.</param>
        /// <param name="dbTVSeriesRepository">Экземпляр репозитория сериалов.</param>
        /// <param name="dbMusicRepository">Экземпляр репозитория музыки.</param>
        /// <param name="dbAuthorisedUserRepository">Экземпляр репозитория пользователей личного кабинета.</param>
        /// <param name="dbEmailRepository">Экземпляр репозитория emails.</param>
        /// <param name="dbGenreRepository">Экземпляр репозитория жанров.</param>
        /// <param name="dbUserRepository">Экземпляр репозитория пользователей.</param>
        /// <param name="dbRoleRepository">Экземпляр репозитория ролей.</param>
        /// <param name="dbPermissionRepository">Экземпляр репозитория разрешений.</param>
        public UnitOfWork(DbContext dbContext,
            IDbReleaseRepository dbReleaseRepository,
            IDbFilmRepository dbFilmRepository,
            IDbTVSeriesRepository dbTVSeriesRepository,
            IDbMusicRepository dbMusicRepository,
            IDbAuthorisedUserRepository dbAuthorisedUserRepository,
            IDbEmailRepository dbEmailRepository,
            IDbGenreRepository dbGenreRepository,
            IDbUserRepository dbUserRepository,
            IDbRoleRepository dbRoleRepository,
            IDbPermissionRepository dbPermissionRepository)
        {
            _dbContext = dbContext;
            ReleaseRepository = dbReleaseRepository;
            FilmRepository = dbFilmRepository;
            TVSeriesRepository = dbTVSeriesRepository;
            MusicRepository = dbMusicRepository;
            UserAuthorisedRepository = dbAuthorisedUserRepository;
            EmailRepository = dbEmailRepository;
            GenreRepository = dbGenreRepository;
            UserRepository = dbUserRepository;
            RoleRepository = dbRoleRepository;
            PermissionRepository = dbPermissionRepository;
        }

        /// <summary>
        /// Сохраняет изменения в хранилище данных.
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Освобождает управляемые ресурсы.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождает управляемые ресурсы.
        /// </summary>
        /// <param name="disposing">Указывает вызван ли этот метод из метода Dispose() или из финализатора.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }

            _disposed = true;
        }

        public int SaveChanges()
        {
            return _rocketContext.SaveChanges();
        }
    }
}
