using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.User
{
    /// <summary>
    /// Представляет модель хранения данных о языке пользователя
    /// </summary>
    public class DbLanguage
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
        /// Задает или возвращает коллекцию дополнительной информации пользователя,
        /// к которой относится этот язык
        /// </summary>
        public ICollection<DbUserDetails> DbUserDetails { get; set; }
    }
}