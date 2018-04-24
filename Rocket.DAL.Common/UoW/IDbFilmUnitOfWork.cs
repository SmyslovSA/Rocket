using Rocket.DAL.Common.Repositories;

namespace Rocket.DAL.Common.UoW
{
    /// <summary>
    /// Представляет unit of work для работы с фильмами
    /// </summary>
    public interface IDbFilmUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Возвращает репозиторий для фильмов
        /// </summary>
        IDbFilmRepository FilmRepository { get; }

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
