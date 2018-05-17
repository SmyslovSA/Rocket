using System;

namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает релиз сериала для целей нотификации
    /// </summary>
    public class TVSeriesForNotification
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер сериала
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает название сериала
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает порядковый номер сезона
        /// </summary>
        public int SeasonNumber { get; set; }

        /// <summary>
        /// Возвращает или задает номер серии в сезоне
        /// </summary>
        public int EpisodeNumber { get; set; }

        /// <summary>
        /// Возвращает или задает дату выхода серии
        /// </summary>
        public DateTime ReleaseDate { get; set; }
    }
}