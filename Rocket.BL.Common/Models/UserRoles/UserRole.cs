using System;
using System.Collections.Generic;

namespace Rocket.BL.Common.Models.UserRoles
{
    public class UserRole
    {
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<UserPermission> Permissions;
    }
}
