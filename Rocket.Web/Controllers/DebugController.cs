using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IHttpActionResult CreateUsers()
        {
            // implement logic here
            return Ok();
        }
    }
}
