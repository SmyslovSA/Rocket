using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("changePersonalArea/genre")]
    public class ChangeGenreManagerServiceController : ApiController
    {
        private IGenreManager _genreManager;

        public ChangeGenreManagerServiceController(IGenreManager emailManager)
        {
            _genreManager = emailManager;
        }

        [HttpPost]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Created, "New model description", typeof(SimpleUser))]
        public IHttpActionResult SaveGenre([FromBody]SimpleUser model, string category, string genre)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be empty");
            }
            else if (string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(category))
            {
                return BadRequest("genre cannot be empty");
            }

            _genreManager.AddGenre(model, category, genre);

            //заменить null за конечный результат , т.к. не билдится проект
            return null; //Created(/*$"____/{model.Id}", model*/);
        }

        [HttpDelete]
        public IHttpActionResult DeleteGenre([FromBody]SimpleUser model, string category, string genre)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be empty");
            }
            else if (string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(category))
            {
                return BadRequest("email or genre cannot be empty");
            }

            _genreManager.DeleteGenre(model, category, genre);
            return Ok();
        }
    }
}