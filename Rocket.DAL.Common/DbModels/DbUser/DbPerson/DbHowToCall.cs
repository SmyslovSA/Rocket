namespace Rocket.DAL.Common.DbModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Представляет модель хранения сведений о том, как обращаться к пользователю
    /// </summary>
    public class DbHowToCall
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор сведений о том, как обращаться к пользователю
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает обращение к пользователю
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию контактных данных,
        /// в которых указано данное обращение к пользователю
        /// </summary>
        public ICollection<DbCommunication> DbCommunications { get; set; }
    }
}
