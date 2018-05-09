using System;

namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения данных сообщения с информацией о совершенных
    /// пользователем либо гостем платежах на сайте (покупка премиум аккаунта, донат)
    /// </summary>
    public class DbBillingMessage
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает получателя сообщения
        /// </summary>
        public DbReceiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает оплаченную сумму
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Возвращает или задает время создания сообщения
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
