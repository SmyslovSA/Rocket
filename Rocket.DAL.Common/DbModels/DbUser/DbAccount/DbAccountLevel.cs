namespace Rocket.DAL.Common.DbModels
{
    using System.Collections.Generic;

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
        /// Возвращает или задает коллекцию аккаунтов,
        /// в которых указан данный уровень аккаунта пользователя
        /// </summary>
        public ICollection<DbAccount> DbAccounts { get; set; }
    }
}
