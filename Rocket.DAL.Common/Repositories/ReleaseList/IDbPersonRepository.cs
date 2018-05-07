using Rocket.DAL.Common.DbModels.DbUser.DbPerson;

namespace Rocket.DAL.Common.Repositories.ReleaseList
{
    /// <summary>
    /// Представляет репозиторий для людей (актёров, режиссёров)
    /// </summary>
    public interface IDbPersonRepository :IRepository<DbPerson>
    {
    }
}
