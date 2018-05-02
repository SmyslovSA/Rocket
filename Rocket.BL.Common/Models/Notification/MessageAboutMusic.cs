namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает сообщение о музыкальном релизе
    /// </summary>
    public class MessageAboutMusic
    {
        /// <summary>
        /// Возвращает или задает получателя сообщения
        /// </summary>
        public Receiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает музыкальный релиз
        /// для целей нотификации
        /// </summary>
        public MusicForNotification Music { get; set; }
    }
}