using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Rocket.DAL.IdentityModule
{
    public class CustomUser : IdentityUser
    {
        public CustomUser(string name)
        {
            UserId = Guid.NewGuid().ToString();
            UserLoginName = name;
        }

        public string UserId { get; }

        public string Name { get; set; }

        public string UserLoginName { get; set; }

        public CustomRole Role { get; set; }
    }
}
