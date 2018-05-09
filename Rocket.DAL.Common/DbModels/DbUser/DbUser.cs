using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.DAL.Common.DbModels.DbUser
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
        /// Возвращает или задает информацию о человеке
        /// </summary>
        public DbPerson.DbPerson Person { get; set; }

        /// <summary>
        /// Возвращает или задает информацию об аккаунте
        /// </summary>
        public DbAccount.DbAccount Account { get; set; }

        /// <summary>
        /// Список ролей юзера
        /// </summary>
        public virtual ICollection<DbRole> Roles { get; set; }
    }
}
