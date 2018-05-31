using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("personal/ganre")]
    public class ChangeGenreController : ApiController
    {
        private IGenreManager _genreManager;
        
        public ChangeGenreController(IGenreManager emailManager)
        {
            _genreManager = emailManager;
        }

        [HttpPost]
        [Route("add/{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Created, "New model description", typeof(SimpleUser))]
        public IHttpActionResult SaveGenre(int idUser, string category, string genre)
        {
            if (idUser != 0)
            {
                if (string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(category))
                {
                    return BadRequest("email or genre cannot be empty");
                }
                else
                {
                    bool result = _genreManager.AddGenre(idUser, category, genre);
                    if (result)
                    {
                        return StatusCode(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.InternalServerError);
                    }

                }

            }
            else
            {
                return BadRequest("idUser can't is empty");

            }
        }

        [HttpDelete]
        [Route("delete/{id:int:min(1)}")]
        public IHttpActionResult DeleteGenre(int idUser, string category, string genre)
        {
            if (idUser != 0)
            {
                if (string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(category))
                {
                    return BadRequest("email or genre cannot be empty");
                }
                else
                {
                    bool result = _genreManager.DeleteGenre(idUser, category, genre);
                    if (result)
                    {
                        return StatusCode(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.InternalServerError);
                    }

                }
            }
            else
            {
                return BadRequest("idUser can't is empty");

            }

        }
    }
}