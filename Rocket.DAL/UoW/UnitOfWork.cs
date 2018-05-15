using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Common.UoW;
using System;
using System.Data.Entity;
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
        private bool disposedValue;

        /// <summary>
        /// Возвращает репозиторий для релизов
        /// </summary>
        public IDbReleaseRepository ReleaseRepository { get; private set; }

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
        /// Возвращает репозиторий для emails
        /// </summary>
        public IDbEmailRepository EmailRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для genre
        /// </summary>
        public IDbGenreRepository GenreRepository { get; private set; }

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
        /// репозиотрий для юзеров
        /// </summary>
        public IDbAuthorisedUserRepository UserAuthorisedRepository { get; private set; }


        /// <summary>
        /// /// <summary>
        /// Создает новый экземпляр <see cref="UnitOfWork"/>
        /// c заданным контекстом данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста данных</param>
        /// <param name="dbReleaseRepository">Экземпляр репозитория релизов</param>
        /// <param name="dbFilmRepository">Экземпляр репозитория фильмов</param>
        /// <param name="dbTVSeriesRepository">Экземпляр репозитория сериалов</param>
        /// <param name="dbMusicRepository">Экземпляр репозитория музыки</param>
        /// <param name="dbAuthorisedUserRepository">Экземпляр репозитория пользователей личного кабинета</param>
        /// <param name="dbEmailRepository">Экземпляр репозитория emails</param>
        /// <param name="dbGenreRepository">Экземпляр репозитория жанров</param>
        /// <param name="dbUserRepository">Экземпляр репозитория пользователей</param>
        /// <param name="dbRoleRepository">Экземпляр репозитория ролей</param>
        /// <param name="dbPermissionRepository">Экземпляр репозитория разрешений</param>
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
        /// Сохраняет изменения в хранилище данных
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
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

                _dbContext?.Dispose();
                _dbContext = null;
                disposedValue = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}