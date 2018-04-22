using System.Collections;

namespace Rocket.BL.Common.Services
{
    public interface IUserRoleService
    {
        /// <summary>
        /// Add new UserRole. return success/failed 
        /// </summary>
        /// <returns></returns>
        bool AddRole(ushort roleId, string roleName);

        /// <summary>
        /// UserRole.IsActive - switch true/false 
        /// </summary>
        void SwitchRoleVisibility(ushort roleId);

        /// <summary>
        /// get totally list
        /// </summary>
        /// <returns></returns>
        IEnumerable FetchAll();
    }
}
