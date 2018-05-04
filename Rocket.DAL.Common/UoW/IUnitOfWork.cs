using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using System;

namespace Rocket.DAL.Common.UoW
{
    /// <summary>
    /// Представляет общий интерфейс unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Возвращает репозиторий для фильмов
        /// </summary>
        IDbFilmRepository FilmRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для сериалов
        /// </summary>
        IDbTVSeriesRepository TVSeriesRepository { get; }

		/// <summary>
		/// Возвращает репозиторий для сериалов
		/// </summary>
		IDbMusicRepository MusicRepository { get; }

        IDbUserRepository UserRepository { get; }
		/// <summary>
		/// Сохраняет изменения в хранилище данных
		/// </summary>
		void Save();
    }
}