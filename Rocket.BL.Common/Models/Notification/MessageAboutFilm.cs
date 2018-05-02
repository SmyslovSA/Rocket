namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает сообщение о релизе фильма
    /// </summary>
    public class MessageAboutFilm
    {
        /// <summary>
        /// Возвращает или задает получателя сообщения
        /// </summary>
        public Receiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает релиз фильма для целей нотификации 
        /// </summary>
        public FilmForNotification Film { get; set; }
    }
}