using Rocket.BL.Common.Models.UserRoles;

namespace Rocket.BL.Common.Models.User
{
    public interface IUser
    {
        IPerson Person { get; set; }

        IAccount Account { get; set; }
    }
}
