using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using System.Data.Entity;

namespace Rocket.DAL.Repositories.PersonalArea
{
    /// <summary>
    /// репозиторий жанров
    /// </summary>
    public class DbGenreRepository : BaseRepository<DbGenre>,IDbGenreRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для жанров с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbGenreRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
