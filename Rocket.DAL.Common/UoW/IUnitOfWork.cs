﻿using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.IDbUserRoleRepository;
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
        /// Возвращает репозиторий для emails.
        /// </summary>
        IDbEmailRepository EmailRepository { get; }

        /// <summary>
        /// Репозиторий для работы с пользователями.
        /// </summary>
        IDbUserRepository UserRepository { get; }

        /// <summary>
        /// Репозиторий для работы с ролями.
        /// </summary>
        IDbRoleRepository RoleRepository { get; }

        /// <summary>
        /// Репозиторий для работы с пермишенами.
        /// </summary>
        IDbPermissionRepository PermissionRepository { get; }

        /// <summary>
        /// Репозиотрий для работы с пользователями личного кабинета.
        /// </summary>
        IDbAuthorisedUserRepository UserAuthorisedRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для музыкального релиза
        /// </summary>
        IBaseRepository<DbMusic> MusicRepository { get; }

        /// <summary>
        /// Репозиторий настроек парсера
        /// </summary>
        IBaseRepository<ParserSettingsEntity> ParserSettingsRepository { get; }

        /// <summary>
        /// Репозиторий ресурса
        /// </summary>
        IBaseRepository<ResourceEntity> ResourceRepository { get; }

        /// <summary>
        /// Репозиторий элемента ресурса
        /// </summary>
        IBaseRepository<ResourceItemEntity> ResourceItemRepository { get; }

        /// <summary>
        /// Репозиторий музыкального жанра
        /// </summary>
        IBaseRepository<DbMusicGenre> MusicGenreRepository { get; }

        /// <summary>
        /// Репозиторий музыкального трека
        /// </summary>
        IBaseRepository<DbMusicTrack> MusicTrackRepository { get; }

        /// <summary>
        /// Репозиторий музыканта
        /// </summary>
        IBaseRepository<DbMusician> MusicianRepository { get; }

        IBaseRepository<CategoryEntity> CategoryRepository { get; }

        IBaseRepository<EpisodeEntity> EpisodeRepository { get; }

        IBaseRepository<GenreEntity> GenreRepository { get; }

        IBaseRepository<PersonEntity> PersonRepository { get; }

        IBaseRepository<PersonTypeEntity> PersonTypeRepository { get; }

        IBaseRepository<SeasonEntity> SeasonRepository { get; }

        IBaseRepository<TvSeriasEntity> TvSeriasRepository { get; }

        /// <summary>
        /// Сохраняет изменения в хранилище данных
        /// </summary>
        int SaveChanges();
    }
}