namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения сводных данных для установления связи
    /// M:M между моделью <see cref="DbReceiver"/> и моделью <see cref="DbMusicMessage"/>
    /// с дополнительным свойством <see cref="Viewed"/>
    /// </summary>
    class ReceiversJoinMusics
    {
        /// <summary>
        /// Возвращает или задает получателя сообщения музыкальном релизе
        /// </summary>
        public DbReceiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер
        /// получателя сообщения о музыкальном релизе
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Возвращает или задает сообщение о музыкальном релизе
        /// </summary>
        public DbMusicMessage MusicMessage { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер сообщения
        /// о музыкальном релизе
        /// </summary>
        public int MusicMessageId { get; set; }

        /// <summary>
        /// Возвращает или задает флаг просмотра пользователем
        /// push нотификации
        /// </summary>
        public bool Viewed { get; set; }
    }
}