using System;
using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.BL.Common.Services
{
    public interface IUserRoleManager : IDisposable
    {
        /// <summary>
        /// Добавить в роль соответствующий пермишен
        /// </summary>
        /// <param name="userId"> Идентификатор ползователя. </param>
        /// <param name="roleId"> Идентификатор ролию </param>
        void AddToRole(int userId, int roleId = 0);

        /// <summary>
        /// Получить все роли пользователя по его Id
        /// </summary>
        /// <param name="userId"> Идентификатор ползователя. </param>
        /// <returns>list</returns>
        IEnumerable<DbRole> GetRoles(int userId);

        /// <summary>
        /// Проверка что у юзера есть соответствующая роль
        /// </summary>
        /// <param name="userId"> Идентификатор ползователя. </param>
        /// <param name="roleId"> Идентификатор ролию </param>
        /// <returns>bool</returns>
        bool IsInRole(int userId, int roleId);

        /// <summary>
        /// Удалить роль у юзера
        /// </summary>
        /// <param name="userId"> Идентификатор ползователя. </param>
        /// <param name="roleId"> Идентификатор ролию </param>
        /// <returns>bool</returns>
        bool RemoveFromRole(int userId, int roleId);
    }
}