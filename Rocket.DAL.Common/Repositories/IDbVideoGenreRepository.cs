using Rocket.DAL.Common.DbModels;

namespace Rocket.DAL.Common.Repositories
{
    /// <summary>
    /// Представляет репозиторий для жанров видео (фильмов, сериалов)
    /// </summary>
    public interface IDbVideoGenreRepository : IRepository<DbVideoGenre>
    {
    }
}
