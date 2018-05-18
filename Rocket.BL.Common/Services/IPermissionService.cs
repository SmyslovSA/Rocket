using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Services
{
    public interface IPermissionService
    {
        /// <summary>
        /// Добавить существующую функц возможность для выбранной роли
        /// </summary>
        void AddPermissionToRole();

        /// <summary>
        /// Удалить функц возможность из текущего списка у роли
        /// </summary>
        void RemovePermissionFromRole();
    }
}