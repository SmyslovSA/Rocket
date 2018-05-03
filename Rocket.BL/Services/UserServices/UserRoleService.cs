using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// установки роли для пользователя
    /// если не указана, то дефолтовая
    /// </summary>
    public class UserRoleService
    {
        private readonly IUser _user;
        private readonly IRole _role;

        public UserRoleService(IUser user, IRole role)
        {
            _user = user;
            _role = role;
        }

        // todo добавить роль по умолчанию в куда-нибудь
        private const IRole DefaultRole = null;

        // принимаем юзера и флаг для действия
        public void RequestUserRole(IUser user, bool changeRole)
        {
            if (changeRole)
                ChangeUserRole(_user, DefaultRole);

            else
                _user?.GetUserRole();
        }

        public void ChangeUserRole(IUser user, IRole role)
        {
            // todo сетапим роль нашему юзверю
            // _user?.SetUserRole(_role);
        }
    }
}
