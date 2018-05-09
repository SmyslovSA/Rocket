using Rocket.DAL.Common.DbModels.DbUser.DbAccount;
using Rocket.DAL.Common.DbModels.DbUser.DbPerson;
using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель хранения данных пользователя
    /// </summary>
    public class DbAuthorisedUser
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// внешний ключ к таблице DbAccount
        /// </summary>
        public int DbAccountId { get; set; }

        /// <summary>
        /// ссылка на DbAccount
        /// </summary>
        public DbAccount DbAccount { get; set; }

        /// <summary>
        /// внешний ключ к таблице DbPersonality
        /// </summary>
        public int DbPersonalityId { get; set; }

        /// <summary>
        /// ссылка на DbPersonality
        /// </summary>
        public DbPersonality DbPersonality { get; set; }

        /// <summary>
        /// относительный путь от корневой папки приложения к изображению аватара пользователя
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// список e-mail пользователя
        /// </summary>
        public DbEmail Email { get; set; }

        /// <summary>
        /// коллекция выбранных жанров пользователя
        /// </summary>
        public DbGenre Genres { get; set; }
    }
}
