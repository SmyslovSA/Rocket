using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Rocket.DAL.IdentityModule
{
    public class CustomUserStore : IUserStore<CustomUser>
    {
        public Task CreateAsync(CustomUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CustomUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<CustomUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomUser user)
        {
            throw new NotImplementedException();
        }
    }
}
