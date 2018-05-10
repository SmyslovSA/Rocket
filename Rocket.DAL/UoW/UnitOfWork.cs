using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.UoW;
using System;
using System.Data.Entity;

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
        /// Возвращает репозиторий для работы с пользователями
        /// </summary>
        public IDbAuthorisedUserRepository UserRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для emails
        /// </summary>
        public IDbEmailRepository EmailRepository { get; private set; }

        /// <summary>
        /// Возвращает репозиторий для genre
        /// </summary>
        public IDbGenreRepository GenreRepository { get; private set; }

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
        public UnitOfWork(DbContext dbContext,
            IDbFilmRepository dbFilmRepository,
            IDbTVSeriesRepository dbTVSeriesRepository,
            IDbMusicRepository dbMusicRepository,
            IDbAuthorisedUserRepository dbAuthorisedUserRepository,
            IDbEmailRepository dbEmailRepository,
            IDbGenreRepository dbGenreRepository)
        {
            this._dbContext = dbContext;
            this.FilmRepository = dbFilmRepository;
            this.TVSeriesRepository = dbTVSeriesRepository;
            this.MusicRepository = dbMusicRepository;
            this.UserRepository = dbAuthorisedUserRepository;
            this.EmailRepository = dbEmailRepository;
            this.GenreRepository = dbGenreRepository;
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