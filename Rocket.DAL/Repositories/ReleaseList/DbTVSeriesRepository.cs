using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories.ReleaseList;
using System.Data.Entity;

namespace Rocket.DAL.Repositories.ReleaseList
{
    /// <summary>
    /// Представляет репозиторий для сериалов
    /// </summary>
    public class DbTVSeriesRepository : BaseRepository<DbTVSeries>, IDbTVSeriesRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для сериалов с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbTVSeriesRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}