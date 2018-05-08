using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbUserRole
{
    public class DbUserRole
    {
        // чекаем конкретного юезра на наличие пермишена

        /// <summary>
        /// уникальный идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// уникальный идентификатор роли пользователя
        /// </summary>
        public ushort RoleId { get; set; }

        /// <summary>
        /// Список пермишенов для соответствующего user Id
        /// </summary>
        public virtual ICollection<DbPermission> Permissions { get; set; }
    }
}
