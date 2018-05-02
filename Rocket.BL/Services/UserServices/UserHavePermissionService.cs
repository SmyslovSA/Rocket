using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Services.UserServices
{
    public class UserHavePermissionService
    {
        private readonly IUser _user;
        private readonly IPermission _permission;

        public UserHavePermissionService(IUser user, IPermission permission)
        {
            _user = user;
            _permission = permission;
        }

        public bool CheckUserHavePermission(IUser user)
        {
            // GetRoleByUser();
            // bool RoleContainsPermission();
            return true;
        }
    }
}
