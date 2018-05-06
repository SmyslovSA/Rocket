using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Common.Repositories.IDbUserRepository
{
    /// <summary>
    /// Представляет репозитарий сведений о том, как называть пользователя
    /// </summary>
    public interface IDbHowToCallRepository : IRepository<DbHowToCall>
    {
    }
}
