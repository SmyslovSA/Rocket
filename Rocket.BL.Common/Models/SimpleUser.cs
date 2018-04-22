using System.Collections.Generic;

namespace Rocket.BL.Common.Models
{
    //авторизованный пользователь с обычными правами
    public class SimpleUser : AuthorisedUser
    {
        /// <summary>
        /// относительный путь к аватаре пользователя
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// коллекция e-mail адресов пользователя
        /// </summary>
        public ICollection<string> Email { get; set; }
        /// <summary>
        /// персональный список ожидания релизов по категориям  
        /// </summary>
        public PersonalizedTape Personalized { get; set; }
    }
}
