using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.ReleaseList
{
    /// <summary>
    /// Представляет модель хранения данных музыкальных исполнителей
    /// </summary>
    public class DbMusician
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор музыканта
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает полное имя музыкального исполнителя (название группы)
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию музыкальных релизов,
        /// которые относятся к данному исполнителю
        /// </summary>
        public ICollection<DbMusic> Musics { get; set; }
    }
}