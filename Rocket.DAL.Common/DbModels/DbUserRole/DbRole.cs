using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbUserRole
{
    public class DbRole
    {
        /// <summary>
        /// Уникальный идентификатор роли пользователя
        /// </summary>
        public ushort Id { get; set; }

        /// <summary>
        /// Название роли пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Свойство позволяет управлять возможностью
        /// менять доступ к использованию роли
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// collection of permissions 
        /// </summary>
        public ICollection<DbPermission> Permissions;
    }
}
