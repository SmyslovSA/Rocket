using Rocket.DAL.Common.DbModels;

namespace Rocket.DAL.Common.Repositories.IDbUserRepository.IDbPersonRepository
{
    /// <summary>
    /// Представляет репозитарий для сведений о человеке пользователя
    /// </summary>
    public interface IDbPersonRepositary : IRepository<DbPerson>
    { 
    }
}
