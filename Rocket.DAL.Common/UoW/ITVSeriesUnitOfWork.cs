using Rocket.DAL.Common.Repositories;

namespace Rocket.DAL.Common.UoW
{
    /// <summary>
    /// Представляет unit of work для работы с сериалами
    /// </summary>
    public interface ITVSeriesUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Возвращает репозиторий для сериалов
        /// </summary>
        IDbTVSeriesRepository TVSeriesRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для сезонов
        /// </summary>
        IDbSeasonRepository SeasonRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для серий
        /// </summary>
        IDbEpisodeRepository EpisodeRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для людей (актёров, режиссёров)
        /// </summary>
        IDbPersonRepository PersonRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для стран
        /// </summary>
        IDbCountryRepository CountryRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для жанров видео (фильмов, сериалов)
        /// </summary>
        IDbVideoGenreRepository VideoGenreRepository { get; }
    }
}
