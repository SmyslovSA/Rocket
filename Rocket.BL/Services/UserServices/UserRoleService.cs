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

        public Role RequestUserRole(IUser user)
        {
            // todo возвращаем роль юзера
            // return _user?.GetUserRole();

            // todo hello crunch
             return new Role();
        }

        // todo добавить роль по умолчанию в куда-нибудь
        private const IRole DefaultRole = null;

        public void ChangeUserRole(IUser user)
        {
            ChangeUserRole(_user, DefaultRole);
        }

        public void ChangeUserRole(IUser user, IRole role)
        {
            // todo сетапим роль нашему юзверю
            // _user?.SetUserRole(_role);
        }
    }
}
