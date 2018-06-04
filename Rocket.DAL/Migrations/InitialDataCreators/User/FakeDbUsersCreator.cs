using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Migrations.InitialDataCreators.User
{
    /// <summary>
    /// Представляет набор сгенерированных данных о пользователях,
    /// в моделях хранинения данных.
    /// </summary>
    public class FakeDbUsersCreator
    {
        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о пользователях.
        /// </summary>
        /// <param name="dbAccountLevels">Коллекция уровней аккаунта пользователей.</param>
        /// <param name="dbAccountStatuses">Коллекция статусов аккаунта пользователей.</param>
        /// <param name="dbCountries">Коллекция всех стран.</param>
        /// <param name="dbGenders">Коллекция сведений о половой идентификации дополнительной информации пользователей.</param>
        /// <param name="dbHowToCalls">Коллекция сведений о том, как надо обращаться к пользователям.</param>
        /// <param name="dbLanguages">Коллекция всех используемых разговорных языков пользователей.</param>
        public FakeDbUsersCreator()
        {
            var roles = new List<DbRole>()
            {
                //new DbRole() { Id = 1, Name = "unregister" },
                //new DbRole() { Id = 2, Name = "user" },
                //new DbRole() { Id = 3, Name = "moderator" },
                //new DbRole() { Id = 4, Name = "admin" },
            };
        }

        /// <summary>
        /// Возвращает коллекцию сгенерированных пользователей.
        /// </summary>
        public List<DbUser> Users { get; }
    }
}