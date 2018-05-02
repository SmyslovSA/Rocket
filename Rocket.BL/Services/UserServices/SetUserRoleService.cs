using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Services.UserServices
{
    public class SetUserRoleService
    {
        private readonly IUser _user;
        private readonly IRole _role;

        private const IRole DefaultRole = null;

        public SetUserRoleService(IUser user, IRole role)
        {
            _user = user;
            _role = role;
        }

        // todo добавить роль по умолчанию в куда-нибудь

        public void ChangeUserRole(IUser user)
        {
            ChangeUserRole(user, DefaultRole);
        }

        public void ChangeUserRole(IUser user, IRole role)
        {
            // todo сетапим роль нашему юзверю
            // user?.SetUserRole(role);
        }
    }
}
