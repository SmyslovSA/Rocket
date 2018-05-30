using Microsoft.AspNet.Identity.EntityFramework;

namespace Rocket.DAL.IdentityModule
{
    public class CustomDbContext : IdentityDbContext<CustomUser>
    {
        public CustomDbContext() : base("IdentityDb")
        {
        }

        public static CustomDbContext Create()
        {
            return new CustomDbContext();
        }
    }
}
