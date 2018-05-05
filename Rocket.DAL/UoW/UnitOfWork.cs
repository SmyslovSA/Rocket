using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Repositories.ReleaseList;
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
        private IDbFilmRepository _dbFilmRepository;
        private IDbTVSeriesRepository _dbTVSeriesRepository;
        private IDbMusicRepository _dbMusicRepository;
        private bool disposedValue = false;

        /// <summary>
        /// Возвращает репозиторий для фильмов
        /// </summary>
        public IDbFilmRepository FilmRepository
        {
            get
            {
                if (this._dbFilmRepository == null)
                {
                    this._dbFilmRepository = new DbFilmRepository(this._dbContext);
                }
                return this._dbFilmRepository;
            }
        }

        /// <summary>
        /// Возвращает репозиторий для сериалов
        /// </summary>
        public IDbTVSeriesRepository TVSeriesRepository
        {
            get
            {
                if (this._dbTVSeriesRepository == null)
                {
                    this._dbTVSeriesRepository = new DbTVSeriesRepository(this._dbContext);
                }
                return this._dbTVSeriesRepository;
            }
        }

        /// <summary>
        /// Возвращает репозиторий для музыки
        /// </summary>
        public IDbMusicRepository MusicRepository
        {
            get
            {
                if (this._dbMusicRepository == null)
                {
                    this._dbMusicRepository = new DbMusicRepository(this._dbContext);
                }
                return this._dbMusicRepository;
            }
        }

        /// <summary>
        /// Создает новый экземпляр <see cref="UnitOfWork"/>
        /// c заданным контекстом данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста данных</param>
        public UnitOfWork(DbContext dbContext)
        {
            this._dbContext = dbContext;
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