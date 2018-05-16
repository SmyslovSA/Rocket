using System.Collections.Generic;

namespace Rocket.BL.Common.Models.PersonalArea
{
    /// <summary>
    /// Авторизованный пользователь с обычными правами.
    /// </summary>
    public class SimpleUser : AuthorisedUser
    {
        /// <summary>
        /// Относительный путь к аватаре пользователя.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Коллекция e-mail адресов пользователя.
        /// </summary>
        public ICollection<Email> Emails { get; set; }

        /// <summary>
        /// Персональный список релизов в виде списка жанров. 
        /// </summary>
        public ICollection<Genre> Genres { get; set; }
    }
}