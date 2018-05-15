using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.User
{
    /// <summary>
    /// Представляет модель хранения данных о стране для пользователя
    /// </summary>
    public class DbCountry
    {
        /// <summary>
        /// Задает или возвращает уникальный идентификатор страны
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задает или возвращает название страны пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию адресов,
        /// в которых указана данная страна
        /// </summary>
        public ICollection<DbAddress> DbAddresses { get; set; }

        /// <summary>
        /// Задает или возвращает коллекцию дополнительной информации пользователя,
        /// к которой относится эта страна
        /// </summary>
        public ICollection<DbUserDetails> DbUserDetails { get; set; }
    }
}