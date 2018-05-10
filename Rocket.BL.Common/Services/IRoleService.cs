using System;
using System.Linq.Expressions;
using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// сетапим роль по умолчанию нашему юзеру
        /// private const Role DefaultRole = null;
        /// </summary>
        void SetDefaultRole();

        /// <summary>
        ///  сетапим выбранную роль нашему юзверю
        /// </summary>
        /// <param name="role"></param>
        void ChangeUserRole(Role role);
    }
}
