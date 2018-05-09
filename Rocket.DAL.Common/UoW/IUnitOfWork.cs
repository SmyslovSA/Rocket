using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.Repositories.User;
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
		/// Возвращает репозиторий для музыки
		/// </summary>
		IDbMusicRepository MusicRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для пользователей
        /// </summary>
        IDbUserRepository UserRepository { get; }

        /// <summary>
        /// Сохраняет изменения в хранилище данных
        /// </summary>
        void Save();
    }
}