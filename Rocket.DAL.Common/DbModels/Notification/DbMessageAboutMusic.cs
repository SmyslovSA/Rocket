using System;
using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения данных о музыкальном релизе
    /// </summary>
    public class DbMessageAboutMusic
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер музыкального релиза
        /// </summary>
        public int MusicId { get; set; }

        /// <summary>
        /// Возвращает или задает название музыкального релиза
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает дату выхода музыкального релиза
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Возвращает или задает имена исполнителей музыкального релиза
        /// </summary>
        public ICollection<string> Musicians { get; set; }

        /// <summary>
        /// Возвращает или задает время создания сообщения
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}