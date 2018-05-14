namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения данных о email адресе получателя нотификации
    /// </summary>
    public class DbEmail
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер email адреса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает получателя, кторому принадлежит email адрес
        /// </summary>
        public DbReceiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер получателя
        /// кторому принадлежит email адрес
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Возвращает или задает строковое представление email адреса
        /// </summary>
        public string EmailTitle { get; set; }
    }
}