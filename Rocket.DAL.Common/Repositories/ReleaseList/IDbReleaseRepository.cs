using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.DAL.Common.Repositories.ReleaseList
{
    /// <summary>
    /// Представляет репозиторий для релизов (фильмов, серий, музыки)
    /// </summary>
    public interface IDbReleaseRepository : IBaseRepository<DbBaseRelease>
    {
    }
}