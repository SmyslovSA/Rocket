using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.IDbUserRoleRepository;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Context;
using System;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private RocketContext _rocketContext;
        private bool _disposed;

        /// <summary>
        /// Unit of Work для RocketConext
        /// </summary>
        /// <param name="rocketContext">Контекст данных</param>
        /// <param name="musicRepository">Репозиторий релиза</param>
        /// <param name="parserSettingsRepository">Репозиторий настроек парсера</param>
        /// <param name="resourceRepository">Репозиторий ресурса</param>
        /// <param name="resourceItemRepository">Репозиторий элемента ресурса</param>
        /// <param name="musicGenreRepository">Репозиторий жанра</param>
        /// <param name="musicTrackRepository">Репозиторий трека</param>
        /// <param name="musicianRepository">Репозиторий исполнителя</param>
        /// <param name="categoryRepository">Репозиторий категорий</param>
        /// <param name="episodeRepository">Репозиторий серий</param>
        /// <param name="genreRepository">Репозиторий жанров</param>
        /// <param name="personRepository">Репозиторий людей - актеров, режиссеров</param>
        /// <param name="personTypeRepository">Репозиторий типов людей</param>
        /// <param name="seasonRepository">Репозиторий сезонов</param>
        /// <param name="tvSeriasRepository">Репозиторий сериалов</param>
        /// <param name="dbFilmRepository">Репозиторий фильмов</param>
        /// <param name="dbEmailRepository">Репозиторий email</param>
        /// <param name="dbUserRepository">Репозиторий пользователей</param>
        /// <param name="dbRoleRepository">Репозиторий ролей</param>
        /// <param name="dbPermissionRepository">Репозиторий разрешений</param>
        /// <param name="dbAuthorisedUserRepository">Репозиторий авторизованных пользователей</param>
        public UnitOfWork(
            RocketContext rocketContext,
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
            IBaseRepository<TvSeriasEntity> tvSeriasRepository,
            IDbEmailRepository dbEmailRepository,
            IDbUserRepository dbUserRepository,
            IDbRoleRepository dbRoleRepository,
            IDbPermissionRepository dbPermissionRepository,
            IDbAuthorisedUserRepository dbAuthorisedUserRepository,
            IBaseRepository<NotificationsLogEntity> notificationsLogRepository)
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
            EmailRepository = dbEmailRepository;
            UserRepository = dbUserRepository;
            RoleRepository = dbRoleRepository;
            PermissionRepository = dbPermissionRepository;
            UserAuthorisedRepository = dbAuthorisedUserRepository;
            NotificationsLogRepository = notificationsLogRepository;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

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

        /// <summary>
        /// Возвращает репозиторий для emails.
        /// </summary>
        public IDbEmailRepository EmailRepository { get; }

        /// <summary>
        /// Репозиторий для работы с пользователями.
        /// </summary>
        public IDbUserRepository UserRepository { get; }

        /// <summary>
        /// Репозиторий для работы с ролями.
        /// </summary>
        public IDbRoleRepository RoleRepository { get; }

        /// <summary>
        /// Репозиторий для работы с пермишенами.
        /// </summary>
        public IDbPermissionRepository PermissionRepository { get; }

        /// <summary>
        /// Репозиотрий для работы с пользователями личного кабинета.
        /// </summary>
        public IDbAuthorisedUserRepository UserAuthorisedRepository { get; }

        /// <inheritdoc />
        /// <summary>
        /// Репозиторий лога нотификации
        /// </summary>
        public IBaseRepository<NotificationsLogEntity> NotificationsLogRepository { get; }

        /// <summary>
        /// Освобождает управляемые ресурсы.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _rocketContext.SaveChanges();
        }
        
        /// <summary>
        /// Освобождает управляемые ресурсы.
        /// </summary>
        /// <param name="disposing">Указывает вызван ли этот метод из метода Dispose() или из финализатора.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            _rocketContext?.Dispose();
            _rocketContext = null;
            _disposed = true;
        }

    }
}
