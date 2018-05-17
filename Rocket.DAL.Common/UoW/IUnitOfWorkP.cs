using System;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.Repositories.ReleaseList;

namespace Rocket.DAL.Common.UoW
{
    /// <summary>
    /// Представляет общий интерфейс unit of work
    /// </summary>
    public interface IUnitOfWorkP : IDisposable
    {
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
        /// Репозиторий музыканта
        /// </summary>
        IRepository<DbMusician> MusicianRepository { get; }

        IRepository<CategoryEntity> CategoryRepository { get; }

        IRepository<EpisodeEntity> EpisodeRepository { get; }

        IRepository<GenreEntity> GenreRepository { get; }

        IRepository<PersonEntity> PersonRepository { get; }

        IRepository<PersonTypeEntity> PersonTypeRepository { get; }

        IRepository<SeasonEntity> SeasonRepository { get; }

        IRepository<TvSeriasEntity> TvSeriasRepository { get; }

        /// <summary>
        /// Сохраняет изменения в хранилище данных
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Dispose context
        /// </summary>
        /// <param name="disposing"></param>
        void Dispose(bool disposing);
    }
}