namespace Rocket.BL.Common.Models
{
    // авторизованный пользователь
    public abstract class AuthorisedUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
