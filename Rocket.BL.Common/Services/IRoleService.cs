using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.BL.Common.Services
{
    public interface IRoleService : IDisposable
    {
        /// <summary>
        /// Удаляем модель по Id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Получаем список ролей с фильтрами и сортировкой
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<Role> Get(Expression<Func<DbRole, bool>> filter = null
            , Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null
            , string includeProperties = "");

        /// <summary>
        /// Получаем роль по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Role GetById(int id);

        /// <summary>
        /// Добавляем новую роль
        /// </summary>
        /// <param name="role"></param>
        void Insert(Role role);

        /// <summary>
        /// Проверка существования данной роли
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool RoleIsExists(Expression<Func<Role, bool>> filter);

        /// <summary>
        /// Обновляем текущую роль
        /// </summary>
        /// <param name="role"></param>
        void Update(Role role);
    }
}