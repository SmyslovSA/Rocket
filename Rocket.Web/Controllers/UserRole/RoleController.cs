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
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult GetAllRoles()
        {
            _roleService.Get(null, null, "Roles");
            return Ok();
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "Data is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK)]
        public IHttpActionResult GetRoleById(int id)
        {
            var model = _roleService.GetById(id);
            return model == null ? (IHttpActionResult)NotFound() : Ok(model);
        }

        [HttpPost]
        [Route("save")]
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
        [Route("update")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public IHttpActionResult UpdateRole([FromBody] Role role)
        {
            _roleService.Update(role);
            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

        [HttpDelete]
        [Route("delete/{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.Accepted)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Data is not valid", typeof(string))]
        public IHttpActionResult DeleteRoleById(int id)
        {
            if (_roleService.GetById(id) == null)
            {
                return BadRequest("The role does not exist");
            }

            _roleService.Delete(id);
            return new StatusCodeResult(HttpStatusCode.Accepted, Request);
        }
    }
}