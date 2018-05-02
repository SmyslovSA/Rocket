using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;

namespace Rocket.BL.Services.UserServices
{
    public class PermissionManagerService : IPermissionService
    {
        private readonly IRole _role;
        private readonly IPermission _permission;

        public PermissionManagerService(IRole role, IPermission permission) // todo add ilogger
        {
            _role = role;
            _permission = permission;
        }

        public void AddPermissionToRole(IRole roleId, IPermission permission)
        {
            // докидываем пермишен в роль
            // roleId?.Add(permission);
        }

        public void RemovePermissionFromRole(IRole roleId, IPermission permission)
        {
            // удаляем пермишен у роли
            // roleId?.Remove(permission);
        }
    }
}
