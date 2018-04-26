namespace Rocket.BL.Common.Models
{
    // авторизованный пользователь
    public abstract class AuthorisedUser
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }
    }
}
