namespace Rocket.BL.Common.Services
{
    public interface IPermissionService
    {
        /// <summary>
        /// Добавить существующую функц возможность для выбранной роли
        /// </summary>
        /// <returns></returns>
        bool AddPermissionToRole(ushort roleId, ushort permissionId);

        /// <summary>
        /// Удалить функц возможность из текущего списка у роли
        /// </summary>
        /// <returns></returns>
        bool RemovePermissionFromRole(ushort roleId, ushort permissionId);
    }
}
