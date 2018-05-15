using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using System.Data.Entity;

namespace Rocket.DAL.Repositories.PersonalArea
{
    /// <summary>
    /// репозиторий пользователей личного кабинета
    /// </summary>
    public class DbAuthorisedUserRepository : BaseRepository<DbAuthorisedUser>, IDbAuthorisedUserRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для пользователей личного кабинета с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbAuthorisedUserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
