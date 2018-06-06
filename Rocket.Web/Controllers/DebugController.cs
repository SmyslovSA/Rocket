using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
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

            await _usermanager.CreateAsync((new DbUser()
            {
                EmailConfirmed = true,
                Email = "adminuser@gmail.com",
                PasswordHash = _usermanager.PasswordHasher.HashPassword("security"),
                PhoneNumber = "+375221133654",
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "adminUser",
                FirstName = "Иван",
                LastName = "Иванов",
                
                DbUserProfile = new DbUserProfile()
                {
                    Email = new Collection<DbEmail>()
                    {
                        new DbEmail()
                        {
                            Name = "secondmail@gmail.com",
                        }
                    },
                },
            })).ConfigureAwait(false);

            await _usermanager.CreateAsync((new DbUser()
            {
                EmailConfirmed = true,
                Email = "userfirst@gmail.com",
                PasswordHash = _usermanager.PasswordHasher.HashPassword("password"),
                PhoneNumber = "+375221159654",
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "firstUser",
                FirstName = "Петр",
                LastName = "Петров",

                DbUserProfile = new DbUserProfile()
                {
                    Email = new Collection<DbEmail>()
                    {
                        new DbEmail()
                        {
                            Name = "lastemail@gmail.com",
                        }
                    },
                },
            })).ConfigureAwait(false);

            await _usermanager.CreateAsync((new DbUser()
            {
                EmailConfirmed = true,
                Email = "second@gmail.com",
                PasswordHash = _usermanager.PasswordHasher.HashPassword("password2"),
                PhoneNumber = "+375221975854",
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                UserName = "secondUser",
                FirstName = "Кирил",
                LastName = "Булатов",

                DbUserProfile = new DbUserProfile()
                {
                    Email = new Collection<DbEmail>()
                    {
                        new DbEmail()
                        {
                            Name = "kiril@gmail.com",
                        }
                    },
                },
            })).ConfigureAwait(false);

            var admin = await _rolemanager.FindByNameAsync("administrator").ConfigureAwait(false);
            admin.Users.Add(new IdentityUserRole()
            {
                RoleId = admin.Id,
                UserId = _usermanager.FindByName("adminUser").Id,
            });
            var user = await _rolemanager.FindByNameAsync("user").ConfigureAwait(false);
            user.Users.Add(new IdentityUserRole()
            {
                RoleId = user.Id,
                UserId = _usermanager.FindByName("firstUser").Id,
            });
            user.Users.Add(new IdentityUserRole()
            {
                RoleId = user.Id,
                UserId = _usermanager.FindByName("secondUser").Id,
            });

            return Ok();
        }
    }
}
