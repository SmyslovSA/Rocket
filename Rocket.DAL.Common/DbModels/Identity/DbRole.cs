using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Rocket.DAL.Common.DbModels.Identity
{
    public class DbRole : IdentityRole<int, DbUserRole>
    {
        /// <summary>
        /// список пермишенов для роли
        /// </summary>
        public virtual ICollection<DbPermission> Permissions { get; set; }

        /// <summary>
        /// Уникальный идентификатор роли пользователя
        /// </summary>
        //public override int Id { get; set; }

        /// <summary>
        /// Название роли пользователя
        /// </summary>
        //public override string Name { get; set; }

        /// <summary>
        /// список юзеров с этой ролью
        /// </summary>
        //public virtual ICollection<User.DbUser> Users { get; set; }
    }
}