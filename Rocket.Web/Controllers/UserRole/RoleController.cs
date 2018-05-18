using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;
using Swashbuckle.Swagger.Annotations;

namespace Rocket.Web.Controllers.UserRole
{
    [RoutePrefix("roles")]
    public class RoleController : ApiController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllRoles()
        {
            _roleService.Get(null, null, "Roles");
            return Ok();
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult GetRoleById(int id)
        {
            var model = _roleService.GetById(id);
            return model == null ? (IHttpActionResult)NotFound() : Ok(model);
        }

        [HttpPost]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Data is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Created, "New Role description", typeof(Role))]
        public IHttpActionResult SaveRole(Role role)
        {
            if (role == null)
            {
                return BadRequest("Model cannot be empty");
            }

            _roleService.Insert(role);
            return Created($"role/{role.RoleId}", role);
        }

        [HttpPut]
        public IHttpActionResult UpdateRole([FromBody] Role role)
        {
            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

        [HttpDelete]
        [Route("{id:int:min(1)")]
        public IHttpActionResult DeleteRoleById(int id)
        {
            if (_roleService.GetById(id) == null)
            {
                return BadRequest("The role not exists");
            }

            _roleService.Delete(id);
            return new StatusCodeResult(HttpStatusCode.Accepted, Request);
        }
    }
}