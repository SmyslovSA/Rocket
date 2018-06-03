using System.Collections.Generic;
using IdentityServer3.Core.Services.InMemory;

namespace Rocket.Web.Owin
{
    public static class Users
    {
        public static List<InMemoryUser> Load()
        {
            return new List<InMemoryUser>()
            {
                new InMemoryUser()
                {
                    Subject = "user123",
                    Username = "JohnDoe",
                    Password = "asdf"
                }
            };
        }
    }
}