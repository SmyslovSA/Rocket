using System;

namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает релиз фильма для целей нотификации
    /// </summary>
    public class UserForNotification
    {
        /// <summary>
        /// Возвращает или задает название фильма
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает дату выхода фильма
        /// </summary>
        public DateTime ReleaseDate { get; set; }
    }
}