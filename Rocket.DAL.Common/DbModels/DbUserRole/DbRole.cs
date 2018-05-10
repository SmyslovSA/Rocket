using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbUserRole
{
    public class DbRole
    {
        // класс для получения всего списка ролей с пропертями и пермишенами

        /// <summary>
        /// Уникальный идентификатор роли пользователя
        /// </summary>
        public int Id { get; set; }

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
        public ICollection<DbPermission> Permissions { get; set; }
    }
}
