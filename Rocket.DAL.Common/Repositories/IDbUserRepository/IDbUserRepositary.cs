using Rocket.DAL.Common.DbModels.DbUser;

namespace Rocket.DAL.Common.Repositories.IDbUserRepository
{
    /// <summary>
    /// Представляет репозитарий для пользователей
    /// </summary>
    public interface IDbUserRepository : IRepository<DbUser>
    {
    }
}