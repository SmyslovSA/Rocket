namespace Rocket.BL.Common.Services
{
    public interface IPermissionService
    {
        /// <summary>
        /// Добавить существующую функц возможность для выбранной роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <param name="idPermission"></param>
        void AddPermissionToRole(int idRole, int idPermission);

        /// <summary>
        /// Удалить функц возможность из текущего списка у роли
        /// </summary>
        /// <param name="idRole"></param>
        /// <param name="idPermission"></param>
        void RemovePermissionFromRole(int idRole, int idPermission);
    }
}