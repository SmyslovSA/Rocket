using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Identity;

namespace Rocket.Web.Controllers
{
    [RoutePrefix("debug")]
    public class DebugController : ApiController
    {
        private readonly RocketUserManager _usermanager;
        private readonly RockeRoleManager _rolemanager;

        public DebugController(RocketUserManager usermanager, RockeRoleManager rolemanager)
        {
            _usermanager = usermanager;
            _rolemanager = rolemanager;
        }

        [Route("users/create")]
        [HttpGet]
        public async Task<IHttpActionResult> CreateUsers()
        {
            await _rolemanager.CreateAsync(new IdentityRole("administrator")).ConfigureAwait(false);
            await _rolemanager.CreateAsync(new IdentityRole("user")).ConfigureAwait(false);
            //var t = await _usermanager.CreateAsync(new IdentityUser()
            //{
            //    EmailConfirmed = true,
            //    TwoFactorEnabled = false,
            //    LockoutEnabled = false,
            //    AccessFailedCount = 0,
            //    UserName = "userIvan"
            //});
            await _usermanager.CreateAsync((new DbUser()
            {
                EmailConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "userIvan2",
                FirstName = "Иван",
                LastName = "Иванов",
                DbUserProfile = new DbUserProfile() { },

            })).ConfigureAwait(false);

            return Ok();
        }
    }
}
