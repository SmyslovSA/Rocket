namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения сводных данных для установления связи
    /// M:M между моделью <see cref="DbReceiver"/> и моделью <see cref="DbFilmMessage"/>
    /// с дополнительным свойством <see cref="Viewed"/>
    /// </summary>
    public class ReceiversJoinFilms
    {
        /// <summary>
        /// Возвращает или задает получателя сообщения о релизе фильма
        /// </summary>
        public DbReceiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер
        /// получателя сообщения о релизе фильма
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Возвращает или задает сообщение о релизе фильма
        /// </summary>
        public DbFilmMessage FilmMessage { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер сообщения о релизе фильма
        /// </summary>
        public int FilmMessageId { get; set; }

        /// <summary>
        /// Возвращает или задает флаг просмотра пользователем
        /// push нотификации
        /// </summary>
        public bool Viewed { get; set; }
    }
}