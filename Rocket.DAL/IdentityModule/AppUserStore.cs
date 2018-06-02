using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Rocket.DAL.IdentityModule
{
    public class AppUserStore : IUserStore<AppUser>
    {
        // todo MP fish

        public Task CreateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
