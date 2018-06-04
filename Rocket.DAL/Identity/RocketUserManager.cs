using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Identity
{
    public class RocketUserManager:UserManager<DbUser>
    {
        public RocketUserManager(IUserStore<DbUser> store) : base(store)
        {
        }
    }

    public class RockeRoleManager: RoleManager<IdentityRole>
    {
        public RockeRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}