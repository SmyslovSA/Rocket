using Rocket.BL.Common.Services;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// Добавление/удаление пермишенов у ролей + логирование
    /// </summary>
    public class PermissionManagerService : IPermissionService
    {
        public void AddPermissionToRole()
        {
            // докидываем пермишен в роль
        }

        public void RemovePermissionFromRole()
        {
            // удаляем пермишен у роли
        }
    }
}