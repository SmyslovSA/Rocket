namespace Rocket.DAL.Common.DbModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Представляет модель хранения данных о личности
    /// </summary>
    public class DbPersonality
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификационный номер сведений о личности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Возвращает или задает человека
        /// </summary>
        public Dbperson Dbperson { get; set; }
    }
}
