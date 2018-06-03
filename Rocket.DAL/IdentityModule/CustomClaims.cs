using Microsoft.AspNet.Identity.EntityFramework;

namespace Rocket.DAL.IdentityModule
{
    public class CustomClaims : IdentityUserClaim
    {
        public int ClaimId { get; set; }

        public string Description { get; set; }
    }
}
