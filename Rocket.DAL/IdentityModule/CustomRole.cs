using Microsoft.AspNet.Identity.EntityFramework;

namespace Rocket.DAL.IdentityModule
{
    public class CustomRole : IdentityRole
    {
        public int IdRole { get; set; }

        public string NameRole { get; set; }
    }
}
