namespace Rocket.BL.Common.Models
{
    // авторизованный пользователь
    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
