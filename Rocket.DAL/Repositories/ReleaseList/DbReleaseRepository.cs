using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories.ReleaseList;
using System.Data.Entity;

namespace Rocket.DAL.Repositories.ReleaseList
{
    /// <summary>
    /// Представляет репозиторий для релизов (фильмов, серий, музыки)
    /// </summary>
    public class DbReleaseRepository : BaseRepository<DbBaseRelease>, IDbReleaseRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для релизов с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbReleaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
