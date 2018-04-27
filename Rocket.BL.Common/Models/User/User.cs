namespace Rocket.BL.Common.Models.User
{
    public class User : IUser
    {
        public IPerson Person { get; set; }

        public IAccount Account { get; set; }
    }
}
