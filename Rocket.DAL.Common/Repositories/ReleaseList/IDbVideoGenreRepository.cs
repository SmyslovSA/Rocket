using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.DAL.Common.Repositories.ReleaseList
{
    /// <summary>
    /// Представляет репозиторий для жанров видео (фильмов, сериалов)
    /// </summary>
    public interface IDbVideoGenreRepository : IBaseRepository<DbVideoGenre>
    {
    }
}