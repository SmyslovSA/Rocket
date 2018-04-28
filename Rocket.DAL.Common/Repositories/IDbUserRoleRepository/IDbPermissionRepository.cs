using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.DAL.Common.Repositories.IDbRoleRepository
{
    /// <summary>
    /// Репозиторий для работы с правами
    /// </summary>
    public interface IDbPermissionRepository : IRepository<DbPermission>
    {
    }
}
