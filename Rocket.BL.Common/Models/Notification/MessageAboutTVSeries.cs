using System;

namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает сообщение о релизе сериала
    /// </summary>
    public class MessageAboutTVSeries
    {
        /// <summary>
        /// Возвращает или задает получателя сообщения
        /// </summary>
        public Receiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает релиз сериала для целей нотификации 
        /// </summary>
        public TVSeriesForNotification TVSeries { get; set; }

        /// <summary>
        /// Возвращает или задает время создания сообщения
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}