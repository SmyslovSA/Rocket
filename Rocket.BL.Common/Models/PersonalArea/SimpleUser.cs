using System.Collections.Generic;

namespace Rocket.BL.Common.Models.PersonalArea
{
    /// <summary>
    /// авторизованный пользователь с обычными правами
    /// </summary>
    public class SimpleUser : AuthorisedUser
    {
        /// <summary>
        /// относительный путь к аватаре пользователя
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// коллекция e-mail адресов пользователя
        /// </summary>
        public ICollection<Email> Emails { get; set; }

        /// <summary>
        /// персональный список релизов в виде списка жанров 
        /// </summary>
        public ICollection<Genre> Genres { get; set; }

        public User.User User { get; set; }
    }
}
