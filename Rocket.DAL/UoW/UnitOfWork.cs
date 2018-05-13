using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Common.UoW;
using System;
using System.Data.Entity;
using Rocket.DAL.Common.Repositories.IDbUserRepository;
using Rocket.DAL.Common.Repositories.IDbUserRoleRepository;

namespace Rocket.DAL.UoW
{
    /// <summary>
    /// Представляет unit of work.
    /// Содержит репозитории использующие один контекст данных
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private bool disposedValue = false;

        /// <summary>
        /// Возвращает репозиторий для фильмов
        /// </summary>
        public IDbFilmRepository FilmRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для сериалов
        /// </summary>
        public IDbTVSeriesRepository TVSeriesRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для музыки
        /// </summary>
        public IDbMusicRepository MusicRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для работы с пользователями Authorized
        /// </summary>
        public IDbAuthorisedUserRepository AuthorizedUserRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для emails
        /// </summary>
        public IDbEmailRepository EmailRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для genre
        /// </summary>
        public IDbGenreRepository GenreRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для пользователя
        /// </summary>
        public IDbUserRepository UserRegRepository { get; private set; }

        /// <summary>
        /// Репозиторий для работы с пользователями
        /// </summary>
        public IDbUserRepository UserRepository { get; private set; }

        /// <summary>
        /// Репозиторий для работы с ролями
        /// </summary>
        public IDbRoleRepository RoleRepository { get; private set; }

        /// <summary>
        /// Репозиторий для работы с пермишенами
        /// </summary>
        public IDbPermissionRepository PermissionRepository { get; private set; }

        /// <summary>
        /// /// <summary>
        /// Создает новый экземпляр <see cref="UnitOfWork"/>
        /// c заданным контекстом данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста данных</param>
        /// <param name="dbFilmRepository">Экземпляр репозитория фильмов</param>
        /// <param name="dbTVSeriesRepository">Экземпляр репозитория сериалов</param>
        /// <param name="dbMusicRepository">Экземпляр репозитория музыки</param>
        /// <param name="dbAuthorisedUserRepository">Экземпляр репозитория пользователей</param>
        /// <param name="dbEmailRepository">Экземпляр репозитория emails</param>
        /// <param name="dbGenreRepository">Экземпляр репозитория жанров</param>
        /// <param name="dbUserRepository">Экземпляр репозитория пользователей</param>
        public UnitOfWork(DbContext dbContext,
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
            this._dbContext = dbContext;
            this.FilmRepository = dbFilmRepository;
            this.TVSeriesRepository = dbTVSeriesRepository;
            this.MusicRepository = dbMusicRepository;
            this.AuthorizedUserRepository = dbAuthorisedUserRepository;
            this.EmailRepository = dbEmailRepository;
            this.GenreRepository = dbGenreRepository;
            this.UserRegRepository = dbUserRepository;
            this.UserRepository = dbUserRepository;
            this.RoleRepository = dbRoleRepository;
            this.PermissionRepository = dbPermissionRepository;
        }

        /// <summary>
		/// Сохраняет изменения в хранилище данных
		/// </summary>
        public void Save()
        {
            this._dbContext.SaveChanges();
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        /// <param name="disposing">Указывает вызван ли этот метод из метода Dispose() или из финализатора</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }

                this._dbContext?.Dispose();
                this._dbContext = null;
                disposedValue = true;
            }
        }

        ~UnitOfWork()
        {
            this.Dispose(false);
        }
    }
}