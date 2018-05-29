using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Rocket.DAL.Context
{
    public class AppUser : IUser
    {
        public AppUser(string name)
        {
            Id = Guid.NewGuid().ToString();
            UserName = name;
        }

        public string Id { get; }
        public string UserName { get; set; }
    }
}
