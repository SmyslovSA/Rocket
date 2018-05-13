﻿using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
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
        /// Возвращает репозиторий для релизов
        /// </summary>
        IDbReleaseRepository ReleaseRepository { get; }

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
        /// Возвращает репозиторий для юзеров
        /// </summary>
        Repositories.IDbUserRepository.IDbUserRepository UserRepository { get; }

        /// <summary>
        /// ВОзвращает репозиторий ролей
        /// </summary>
        Repositories.IDbUserRoleRepository.IDbRoleRepository RoleRepository { get; }

        /// <summary>
        /// Возвращает репозиторий пермишенов
        /// </summary>
        Repositories.IDbUserRoleRepository.IDbPermissionRepository PermissionRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для emails
        /// </summary>
        IDbEmailRepository EmailRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для genre
        /// </summary>
        IDbGenreRepository GenreRepository { get; }

        /// Возвращает репозиторий для пользователей
        /// </summary>
        IDbUserRepository UserRegRepository { get; }

        /// <summary>
        /// Сохраняет изменения в хранилище данных
        /// </summary>
        void Save();
    }
}