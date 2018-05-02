using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// Добавление/удаление пермишенов у ролей + логирование
    /// </summary>
    public class PermissionManagerService : IPermissionService
    {
        private readonly IRole _role;
        private readonly IPermission _permission;

        public PermissionManagerService(IRole role, IPermission permission) // todo add ilogger
        {
            _role = role;
            _permission = permission;
        }

        public void AddPermissionToRole(IRole role, IPermission permission)
        {
            // докидываем пермишен в роль
            // role?.Add(permission);
        }

        public void RemovePermissionFromRole(IRole role, IPermission permission)
        {
            // удаляем пермишен у роли
            // role?.Remove(permission);
        }
    }
}
