using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Services.UserServices
{
    public class UserHasPermissionService
    {
        private readonly IUser _user;
        private readonly IPermission _permission;

        public UserHasPermissionService(IUser user, IPermission permission)
        {
            _user = user;
            _permission = permission;
        }

        public bool CheckUserHasPermission()
        {
            // получаем ответ по пермишену для юзера
            // return _user?.HasPermission(_permission);

            // todo hello crunch 
            return true;
        }
    }
}
