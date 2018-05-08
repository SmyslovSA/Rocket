﻿using Rocket.DAL.Common.Repositories;
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
        /// Сохраняет изменения в хранилище данных
        /// </summary>
        void Save();
    }
}