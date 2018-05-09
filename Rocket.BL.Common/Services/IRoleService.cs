using System;
using System.Linq.Expressions;
using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Меняет значение IsActive для роли на противоположное
        /// </summary>
        void SwitchRoleActivity(Role role);



        /// <summary>
        /// сетапим роль по умолчанию нашему юзеру
        /// </summary>
        void ChangeUserRole();

        /// <summary>
        ///  сетапим выбранную роль нашему юзверю
        /// </summary>
        /// <param name="role"></param>
        void ChangeUserRole(Role role);

        /// <summary>
        /// возвращает Роль по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Role GerRoleById(int id);

        /// <summary>
        /// Добавляет новую роль в хранилище данных
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        void AddNewRole(Role role);

        /// <summary>
        /// Обновляет существующую роль в хранилище
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        void UpdateRole(Role role);

        /// <summary>
        /// Удаляет роль по Id из хранилища данных
        /// </summary>
        /// <param name="id"></param>
        void DeleteRole(int id);

        /// <summary>
        /// проверяем наличие в хранилище данных роли с указанным фильтом
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool RoleIsExists(Expression<Func<Role, bool>> filter);
    }
}
