using System;

namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения данных о сообщении о релизе фильма
    /// </summary>
    public class DbMessageAboutFilm
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер фильма
        /// </summary>
        public int FilmId { get; set; }

        /// <summary>
        /// Возвращает или задает название фильма
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает дату выхода фильма
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Возвращает или задает время создания сообщения
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}