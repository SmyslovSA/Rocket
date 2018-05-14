using System.Net;
using System.Web.Http;
using Rocket.BL.Common.Services.PersonalArea;
using Swashbuckle.Swagger.Annotations;
using Rocket.BL.Common.Models.PersonalArea;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("personal/change/email")]
    public class ChangeEmailController : ApiController
    {
        private IEmailManager _emailEmailManager;

        public ChangeEmailController(IEmailManager emailManager)
        {
            _emailEmailManager = emailManager;
        }

        [HttpPost]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Created, "New model description", typeof(SimpleUser))]
        public IHttpActionResult SaveEmail([FromBody]SimpleUser model,string email)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be empty");
            }
            else if(string.IsNullOrEmpty(email))
            {
                return BadRequest("email cannot be empty");
            }

            _emailEmailManager.AddEmail(model, email);

            //заменить null за конечный результат , т.к. не билдится проект

            return null; //Created(/*$"____/{model.Id}", model*/);
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmail([FromBody]SimpleUser model, string email)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be empty");
            }
            else if (string.IsNullOrEmpty(email))
            {
                return BadRequest("email cannot be empty");
            }

            _emailEmailManager.DeleteEmail(model, email);
            return Ok();
        }
    }

    
}