using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения данных о получателе сообщения
    /// </summary>
    public class DbReceiver
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер получателя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сообщений о платежах,
        /// получателем которых является пользователь
        /// </summary>
        public ICollection<DbBillingMessage> BillingMessages { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сообщений произвольного содержания,
        /// получателем которых является пользователь
        /// </summary>
        public ICollection<DbCustomMessage> CustomMessages { get; set; }
    }
}