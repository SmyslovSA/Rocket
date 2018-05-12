﻿using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Services
{
    public interface IPermissionService
    {
        /// <summary>
        /// Добавить существующую функц возможность для выбранной роли
        /// </summary>
        /// <returns></returns>
        void AddPermissionToRole();

        /// <summary>
        /// Удалить функц возможность из текущего списка у роли
        /// </summary>
        /// <returns></returns>
        void RemovePermissionFromRole();
    }
}
