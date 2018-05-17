using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.DAL.Common.DbModels.User
{
    /// <summary>
    /// Представляет модель хранения данных о пользователе
    /// </summary>
    public class DbUser
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Возвращает или задает логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Возвращает или задает пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Возвращает или задает статус аккаунта
        /// (активирован, не активирован, деактивирован, забанен и так далее)
        /// </summary>
        public virtual DbAccountStatus AccountStatus { get; set; }

        /// <summary>
        /// Возвращает или задает уровень пользователя
        /// (пока что это - обычный и премиум пользователь)
        /// </summary>
        public virtual DbAccountLevel AccountLevel { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию ролей пользователя
        /// </summary>
        public virtual ICollection<DbRole> Roles { get; set; } = new Collection<DbRole>();

        /// <summary>
        /// Возвращает или задает детальную информация пользователя
        /// </summary>
        public virtual DbUserDetails UserDetails { get; set; }
    }
}