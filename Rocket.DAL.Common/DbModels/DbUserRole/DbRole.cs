using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbUserRole
{
    public class DbRole
    {
        /// <summary>
        /// Уникальный идентификатор роли пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название роли пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// список пермишенов для роли
        /// </summary>
        public virtual ICollection<DbPermission> Permissions { get; set; }

        /// <summary>
        /// список юзеров с этой ролью
        /// </summary>
        public virtual ICollection<User.DbUser> Users { get; set; }
    }
}