using FluentValidation;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.Web.Properties;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;

namespace Rocket.Web.Controllers.PersonalArea
{
    [RoutePrefix("personal/genres")]
    public class ChangeGenreController : ApiController
    {
        private readonly IGenreManager _genreManager;

        public ChangeGenreController(IGenreManager genreManager)
        {
            _genreManager = genreManager;
        }

        [HttpGet]
        [Route("musics")]
        public IHttpActionResult GetAllMusicGenres()
        {
           var musicGenres = _genreManager.GetAllMusicGenres();
           return musicGenres == null ? (IHttpActionResult)NotFound() : Ok(musicGenres);
        }

        [HttpGet]
        [Route("tv")]
        public IHttpActionResult GetAllTvGenres()
        {
            var tvGenres = _genreManager.GetAllTvGenres();
            return tvGenres == null ? (IHttpActionResult)NotFound() : Ok(tvGenres);
        }

        [HttpPut]
        [Route("music/add/{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Genre is not valid", typeof(string))]
        public IHttpActionResult SaveMusicGenre(int id, string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest(Resources.EmptyGenre);
            }

            try
            {
                _genreManager.AddMusicGenre(id, genre);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("music/delete/{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Genre is not valid", typeof(string))]
        public IHttpActionResult DeleteMusicGenre(int id, string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest(Resources.EmptyGenre);
            }

            try
            {
                _genreManager.DeleteMusicGenre(id, genre);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("tv/add/{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Genre is not valid", typeof(string))]
        public IHttpActionResult SaveTvGenre(int id, string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest(Resources.EmptyGenre);
            }

            try
            {
                _genreManager.AddTvGenre(id, genre);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("tv/delete/{id:int:min(1)}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Genre is not valid", typeof(string))]
        public IHttpActionResult DeleteTvGenre(int id, string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest(Resources.EmptyGenre);
            }

            try
            {
                _genreManager.DeleteTvGenre(id, genre);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}