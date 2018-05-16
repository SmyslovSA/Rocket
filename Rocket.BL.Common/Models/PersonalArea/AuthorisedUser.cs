namespace Rocket.BL.Common.Models.PersonalArea
{
    /// <summary>
    /// Авторизованный пользователь.
    /// </summary>
    public abstract class AuthorisedUser
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }
    }
}