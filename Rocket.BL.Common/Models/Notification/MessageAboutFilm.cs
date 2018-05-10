namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает сообщение о релизе фильма
    /// </summary>
    public class MessageAboutUser
    {
        /// <summary>
        /// Возвращает или задает получателя сообщения
        /// </summary>
        public Receiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает релиз фильма для целей нотификации 
        /// </summary>
        public UserForNotification User { get; set; }
    }
}