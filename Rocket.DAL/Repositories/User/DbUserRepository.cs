using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Context;

namespace Rocket.DAL.Repositories.User
{
    /// <summary>
    /// Представляет репозиторий для пользователей.
    /// </summary>
    public class DbUserRepository : BaseRepository<DbUser>, IDbUserRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для пользователей
        /// с заданным контекстом базы данных.
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbUserRepository(RocketContext dbContext)
            : base(dbContext)
        {
        }
    }
}