using System;
using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения данных о сообщении о релизе фильма
    /// </summary>
    public class DbFilmMessage
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию объектов, содержащих 
        /// сводные данные о получателе и сообщении о релизе фильма
        /// </summary>
        public ICollection<ReceiversJoinFilms> ReceiversJoinFilms { get; set; }

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