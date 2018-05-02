namespace Rocket.DAL.Common.DbModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Представляет модель хранения данных о стране для пользователя
    /// </summary>
    public class Dbcountry
    {
        /// <summary>
        /// Задает или возвращает уникальный идентификатор языка пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задает или возвращает название языка пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию адресов,
        /// в которых указана данная страна
        /// </summary>
        public ICollection<DbAddress> DbAddresses {get; set;}

        /// <summary>
        /// Возвращает или задает коллекцию локализаций,
        /// в которых указана данная страна
        /// </summary>
        public ICollection<DbLocalization> DbLocalizations { get; set; }
    }
}
