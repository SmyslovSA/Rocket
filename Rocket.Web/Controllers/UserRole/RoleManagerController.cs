using System.Net;
using System.Web.Http;
using Rocket.BL.Common.Services;
using Swashbuckle.Swagger.Annotations;

namespace Rocket.Web.Controllers.UserRole
{
    [RoutePrefix("user")]
    public class RoleManagerController : ApiController
    {
        private readonly IUserRoleManager _roleManager;

        public RoleManagerController(IUserRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("add/role/{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "Data is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "Role added to user")]
        public IHttpActionResult AddToRole(int userId, int roleId) 
        {
             return _roleManager.IsInRole(userId, roleId) ? (IHttpActionResult)NotFound() : Ok();
        }

        [HttpDelete]
        [Route("remove/role/{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "Data is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "Role removed from user")]
        public IHttpActionResult RemoveFromRole(int userId, int roleId) 
        {
            return !_roleManager.IsInRole(userId, roleId) ? (IHttpActionResult)NotFound() : Ok();
        }

        [HttpGet]
        [Route("{id:int:min(1)}/roles")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "Data is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult GetRoles(int userId)
        {
            return _roleManager.GetRoles(userId) == null ? (IHttpActionResult)NotFound() : Ok();
        }

        [HttpGet]
        [Route("has/role")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "Data is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult IsInRole(int userId, int roleId)
        {
            _roleManager.IsInRole(userId, roleId);
            return Ok();
        }
    }
}
