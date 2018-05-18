using System.Web.Http;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;

namespace Rocket.Web.Controllers.UserRole
{
    [RoutePrefix("user/role")]
    public class RoleManagerController : ApiController
    {
        private readonly IUserRoleManager _roleManager;

        public RoleManagerController(IUserRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddToRole() 
        {
            // todo [FromBody]User user, [FromBody]Role role -- принимает метод
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult RemoveFromRole() 
        {
            // todo [FromBody]User user, [FromBody]Role role -- принимает метод
            return Ok();
        }

        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetRoles(int userId)
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult IsInRole(int userId, int roleId)
        {
            return Ok();
        }
    }
}
