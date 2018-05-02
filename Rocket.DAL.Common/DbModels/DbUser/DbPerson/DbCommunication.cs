namespace Rocket.DAL.Common.DbModels
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Представляет модель хранения контактных данных человека
    /// </summary>
    public class DbCommunication
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификационный номер контактной информации
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задает или возвращает сведения о том, как обращаться к пользователю
        /// </summary>
        public DbHowToCall HowToCall { get; set; }

        /// <summary>
        /// Задает или возвращает коллекцию телефонных номеров пользователя
        /// </summary>
        public ICollection<string> Phones { get; set; }

        /// <summary>
        /// Задает или возвращает коллекцию Email
        /// </summary>
        public ICollection<string> EMailAddresses { get; set; }

        /// <summary>
        /// Возвращает или задает почтовый адрес пользователя
        /// </summary>
        public DbAddress MailAddress { get; set; }
    }
}
