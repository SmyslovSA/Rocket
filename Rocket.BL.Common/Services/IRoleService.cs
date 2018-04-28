using System.Collections;

namespace Rocket.BL.Common.Services
{
    public interface IRoleService
    {
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


    }
}