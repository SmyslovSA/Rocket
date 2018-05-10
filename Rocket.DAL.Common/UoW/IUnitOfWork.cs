<<<<<<< HEAD
﻿using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.ReleaseList;
=======
﻿using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.Repositories.User;
>>>>>>> feature/ROC-27_Регистрация_пользователя
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
<<<<<<< HEAD
        /// Возвращает репозиторий для авторизованного пользователя
        /// </summary>
        IDbAuthorisedUserRepository UserRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для emails
        /// </summary>
        IDbEmailRepository EmailRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для genre
        /// </summary>
        IDbGenreRepository GenreRepository { get; }
=======
        /// Возвращает репозиторий для пользователей
        /// </summary>
        IDbUserRepository UserRepository { get; }
>>>>>>> feature/ROC-27_Регистрация_пользователя

        /// <summary>
        /// Сохраняет изменения в хранилище данных
        /// </summary>
        void Save();
    }
}