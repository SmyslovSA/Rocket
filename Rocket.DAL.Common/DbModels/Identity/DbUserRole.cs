using Microsoft.AspNet.Identity.EntityFramework;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.DAL.Common.DbModels.Identity
{
    public class DbUserRole : IdentityUserRole<int>
    {
        /// <summary>
        ///  Get/set userid property
        /// </summary>
        public override int UserId { get; set; }

        /// <summary>
        /// Get/Set roleid property
        /// </summary>
        public override int RoleId { get; set; }

        public virtual DbUser User { get; set; }

        public virtual DbRole Role { get; set; }
    }
}