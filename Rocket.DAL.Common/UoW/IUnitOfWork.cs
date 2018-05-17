using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.Repositories.User;
using System;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.Repositories.IDbUserRoleRepository;

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
        /// Возвращает репозиторий для музыкального релиза
        /// </summary>
        IRepository<DbMusic> MusicRepository { get; }

        /// <summary>
        /// Репозиторий настроек парсера
        /// </summary>
        IRepository<ParserSettingsEntity> ParserSettingsRepository { get; }

        /// <summary>
        /// Репозиторий ресурса
        /// </summary>
        IRepository<ResourceEntity> ResourceRepository { get; }

        /// <summary>
        /// Репозиторий элемента ресурса
        /// </summary>
        IRepository<ResourceItemEntity> ResourceItemRepository { get; }

        /// <summary>
        /// Репозиторий музыкального жанра
        /// </summary>
        IRepository<DbMusicGenre> MusicGenreRepository { get; }

        /// <summary>
        /// Репозиторий музыкального трека
        /// </summary>
        IRepository<DbMusicTrack> MusicTrackRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для юзеров
        /// </summary>
        IDbUserRepository UserRepository { get; }

        /// <summary>
        /// ВОзвращает репозиторий ролей
        /// </summary>
        IDbRoleRepository RoleRepository { get; }

        /// <summary>
        /// Возвращает репозиторий пермишенов
        /// </summary>
        IDbPermissionRepository PermissionRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для emails
        /// </summary>
        IDbEmailRepository EmailRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для genre
        /// </summary>
        IDbGenreRepository GenreRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для пользователей
        /// </summary>
        IDbAuthorisedUserRepository UserAuthorisedRepository { get; }

        IDbTVSeriesRepository TVSeriesRepository { get; }

        /// <summary>
        /// Сохраняет изменения в хранилище данных
        /// </summary>
        void Save();
    }
}