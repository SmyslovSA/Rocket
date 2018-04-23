﻿using System.Collections;

namespace Rocket.BL.Common.Services
{
    public interface IUserRoleService
    {
        /// <summary>
        /// Устанавливает дефолт значение роли для новых 
        /// зарегестрированных пользователей
        /// </summary>
        /// <param name="roleId"></param>
        void SetDefaultUserRule(ushort roleId);

        /// <summary>
        /// Меняет значение роли, установленное вручную
        /// </summary>
        /// <param name="newRoleId"></param>
        void ChangeUserRole(ushort newRoleId);

        /// <summary>
        /// Меняет значение IsActive для роли на противоположное
        /// </summary>
        void SwitchRoleVisibility(ushort roleId);

        /// <summary>
        /// Получить список прав по роли
        /// </summary>
        /// <returns></returns>
        IEnumerable FetchAllByRole(ushort roleId);

        /// <summary>
        /// Добавить функц возможность для роли
        /// </summary>
        /// <returns></returns>
        bool AddRoleToUserRoleList(ushort roleId, ushort permissionId);

        /// <summary>
        /// Удалить функц возможность у роли
        /// </summary>
        /// <returns></returns>
        bool RemoveRoleFromUserRoleList(ushort roleId, ushort permissionId);
    }
}
