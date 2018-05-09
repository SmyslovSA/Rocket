using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Common.Repositories.IDbUserRepository
{
    /// <summary>
    /// Представляет репозитарий для пользователей
    /// </summary>
    public interface IDbUserRepository : IRepository<DbUser>
    {
    }
}