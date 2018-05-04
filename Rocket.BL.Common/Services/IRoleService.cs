using System.Collections;
using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Меняет значение IsActive для роли на противоположное
        /// </summary>
        void SwitchRoleVisibility(IRole role);

        /// <summary>
        /// Получить список прав по роли
        /// </summary>
        /// <returns></returns>
        IEnumerable FetchAllByRole(IRole role);
    }
}