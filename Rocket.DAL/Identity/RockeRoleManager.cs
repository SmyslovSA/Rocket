using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Rocket.DAL.Identity
{
    public class RockeRoleManager : RoleManager<IdentityRole>
    {
        public RockeRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}