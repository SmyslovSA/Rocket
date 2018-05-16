using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.User
{
    /// <summary>
    /// Представляет модель хранения данных об уровне аккаунта пользователя
    /// </summary>
    public class DbAccountLevel
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор уровня аккаунта пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает название уровня аккаунта пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию прльзователей,
        /// в которых указан данный уровень пользователя
        /// </summary>
        public ICollection<DbUser> DbUsers { get; set; }
    }
}