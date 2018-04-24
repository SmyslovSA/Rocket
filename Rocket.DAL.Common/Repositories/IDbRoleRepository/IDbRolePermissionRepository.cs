using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.DAL.Common.Repositories.IDbRoleRepository
{
    /// <summary>
    /// Репозиторий для работы с правами
    /// </summary>
    interface IDbRolePermissionRepository : IRepository<DbRolePermission>
    {
    }
}
