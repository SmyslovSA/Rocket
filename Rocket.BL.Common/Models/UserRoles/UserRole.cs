using System;
using System.Collections.Generic;

namespace Rocket.BL.Common.Models.UserRoles
{
    public class UserRole
    {
        /// <summary>
        /// unique identificator
        /// </summary>
        public ushort RoleId { get; set; }

        /// <summary>
        /// name of role in app
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date when role was created
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// ability switch of using some role
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// collection of permissions 
        /// </summary>
        public ICollection<RolePermission> Permissions;

        public UserRole()
        {
            CreateDate = DateTime.Now;
        }

        public UserRole(ushort id, string name) : this()
        {
            RoleId = id;
            Name = name;
            IsActive = false;
        }
    }


}
