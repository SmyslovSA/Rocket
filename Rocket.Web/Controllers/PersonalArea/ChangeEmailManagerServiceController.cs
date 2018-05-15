using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("changePersonalArea/email")]
    public class ChangeEmailManagerServiceController : ApiController
    {
        private IEmailManager _emailEmailManager;

        public ChangeEmailManagerServiceController(IEmailManager emailManager)
        {
            _emailEmailManager = emailManager;
        }

        [HttpPost]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Created, "New model description", typeof(SimpleUser))]
        public IHttpActionResult SaveEmail([FromBody]SimpleUser model, string email)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be empty");
            }
            else
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("email cannot be empty");
                }
            }

            _emailEmailManager.AddEmail(model, email);

            //todo заменить null за конечный результат , т.к. не билдится проект

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