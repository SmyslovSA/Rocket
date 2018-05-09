using System.Collections;
using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Меняет значение IsActive для роли на противоположное
        /// </summary>
        void SwitchRoleVisibility(Role role);


        // todo добавить роль по умолчанию в куда-нибудь
        // private const IRole DefaultRole = null;

        // public void ChangeUserRole(IUser user)
        // {
        //    ChangeUserRole(user, DefaultRole);
        // }

        // public void ChangeUserRole(IUser user, IRole role)
        // {
        //    // todo сетапим роль нашему юзверю
        //    // _user?.SetUserRole(_role);
        // }

    }
}