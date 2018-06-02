using Microsoft.AspNet.Identity;

namespace Rocket.DAL.IdentityModule
{
    public class CustomRoleManager : RoleManager<CustomRole>
    {
        public CustomRoleManager(IRoleStore<CustomRole, string> store) : base(store)
        {
        }
    }
}
