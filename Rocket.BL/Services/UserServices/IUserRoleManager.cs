using System;
using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.BL.Services.UserServices
{
    public interface IUserRoleManager : IDisposable
    {
        /// <summary>
        /// Добавить в роль соответствующий пермишен
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        void AddToRole(int userId, int roleId = 0);

        /// <summary>
        /// Получить все роли пользователя по его Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<DbRole> GetRoles(int userId);

        /// <summary>
        /// Проверка что у юзера есть соответствующая роль
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool IsInRole(int userId, int roleId);

        /// <summary>
        /// Удалить роль у юзера
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool RemoveFromRole(int userId, int roleId);
    }
}