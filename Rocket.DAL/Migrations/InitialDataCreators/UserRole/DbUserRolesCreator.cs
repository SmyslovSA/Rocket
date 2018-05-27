using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.DAL.Migrations.InitialDataCreators.UserRole
{
    /// <summary>
    /// Формирует коллекции первоначальных данных ролей пользователя
    /// для заполнения ими соответствующего репозитация.
    /// </summary>
    public class DbUserRolesCreator
    {
        /// <summary>
        /// Возвращает коллекцию сведений о ролях пользователей.
        /// </summary>
        public List<DbRole> Items { get; } = new List<DbRole>()
        {
            new DbRole() { Name = "unregister" },
            new DbRole() { Name = "user" },
            new DbRole() { Name = "moderator" },
            new DbRole() { Name = "admin" }
        };
    }
}
