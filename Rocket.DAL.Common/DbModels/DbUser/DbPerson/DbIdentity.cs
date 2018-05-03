using System;

namespace Rocket.DAL.Common.DbModels.DbUser.DbPerson
{ 
    /// <summary>
    /// Представляет модель хранения данных о персоналии пользователя
    /// </summary>
    public class DbIdentity
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификационный номер сведений о личности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задает или возвращает дату рождения пользователя
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Задает или возвращает пол пользователя
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Возвращает или задает человека (компонент пользователя),
        /// в которых указан данная индивидум
        /// </summary>
        public DbPerson DbPerson { get; set; }
    }
}
