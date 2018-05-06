using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Common.Repositories.IDbUserRepository
{
    /// <summary>
    /// Представляет репозитарий для пользователей
    /// </summary>
    public interface IDbUserRepository : IRepository<DbUser>
    {
        DbUser GetByUserLoginFromStore(string login);
        
        void AddUserToStore(DbUser dbUser);

        void UpdateUserInStore(DbUser dbUser);

        void DeleteByUserIdFromStore(int Id);
    }
}