using System.Web.Http;
using Rocket.BL.Common.Models.User;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Services.UserServices;

namespace Rocket.Web.Controllers.UserRole
{
    [RoutePrefix("user/role")]
    public class RoleManagerController : ApiController
    {
        private readonly UserRoleManager _roleManager;

        public RoleManagerController(UserRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddToRole([FromBody]User user, [FromBody]Role role)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult RemoveFromRole([FromBody]User user, [FromBody]Role role)
        {
            return Ok();
        }

        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetRoles([FromBody]User user)
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult IsInRole([FromBody]User user, [FromBody]Role role)
        {
            return Ok();
        }
    }
}
