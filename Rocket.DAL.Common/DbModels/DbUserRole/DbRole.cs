using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbUserRole
{
    public class DbRole
    {
        // класс содержащий в себе всю информацию об уникальной роли

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
        /// список пермишенов для роли
        /// </summary>
        public virtual ICollection<DbPermission> Permissions { get; set; }

        /// <summary>
        /// список юзеров с этой ролью
        /// </summary>
        public virtual ICollection<DbPersonalArea.DbUser> Users { get; set; }
    }
}
