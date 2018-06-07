using FluentValidation;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.Web.Properties;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;

namespace Rocket.Web.Controllers.PersonalArea
{
    public class GenresController : ApiController
    {
        private readonly IGenreManager _genreManager;

        public GenresController(IGenreManager genreManager)
        {
            _genreManager = genreManager;
        }

        [HttpGet]
        [Route("genres/all/music")]
        public IHttpActionResult GetAllMusicGenres()
        {
           var musicGenres = _genreManager.GetAllMusicGenres();
           return musicGenres == null ? (IHttpActionResult)NotFound() : Ok(musicGenres);
        }

        [HttpGet]
        [Route("genres/all/tv")]
        public IHttpActionResult GetAllTvGenres()
        {
            var tvGenres = _genreManager.GetAllTvGenres();
            return tvGenres == null ? (IHttpActionResult)NotFound() : Ok(tvGenres);
        }

        [HttpPut]
        [Route("personal/genres/music/add/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Genre is not valid", typeof(string))]
        public IHttpActionResult SaveMusicGenre(string id, string genre)
        {
            if (string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(id))
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
        [Route("personal/genres/music/delete/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Genre is not valid", typeof(string))]
        public IHttpActionResult DeleteMusicGenre(string id, string genre)
        {
            if (string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(id))
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
        [Route("personal/genres/tv/add/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Genre is not valid", typeof(string))]
        public IHttpActionResult SaveTvGenre(string id, string genre)
        {
            if (string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(id))
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
        [Route("personal/genres/tv/delete/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Genre is not valid", typeof(string))]
        public IHttpActionResult DeleteTvGenre(string id, string genre)
        {
            if (string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(id))
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