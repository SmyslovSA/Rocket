using Rocket.DAL.Common.DbModels;

namespace Rocket.DAL.Common.Repositories
{
    /// <summary>
    /// Представляет репозиторий для людей (актёров, режиссёров)
    /// </summary>
    public interface IDbPersonRepository :IRepository<DbPerson>
    {
    }
}
