using System.Collections.Generic;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Представляет точку входа для контактной информации
    /// </summary>
    public class Communication
    {   
        /// <summary>
        /// Задает или возвращает сведения о том, как обращаться к пользователю
        /// </summary>
        public HowToCall HowToCall { get; set; }
        
        /// <summary>
        /// Задает или возвращает коллекцию телефонных номеров
        /// </summary>
        public ICollection<string> Phones { get; set; }

        /// <summary>
        /// Задает или возвращает коллекцию Eamil
        /// </summary>
        public ICollection<string> EMailAddresses { get; set; }

        /// <summary>
        /// Возвращает или задает почтовый адрес
        /// </summary>
        public Address MailAddress { get; set; }
    }
}
