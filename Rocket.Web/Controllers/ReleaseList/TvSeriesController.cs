using Rocket.BL.Common.Services.ReleaseList;
using System.Web.Http;

namespace Rocket.Web.Controllers.ReleaseList
{
    [RoutePrefix("tvseries")]
    public class TvSeriesController : ApiController
    {
        private readonly ITvSeriesDetailedInfoService _tvSeriesDetailedInfoService;

        public TvSeriesController(ITvSeriesDetailedInfoService tvSeriesDetailedInfoService)
        {
            _tvSeriesDetailedInfoService = tvSeriesDetailedInfoService;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult GetTvSeriesById(int id)
        {
            var tvSeries = _tvSeriesDetailedInfoService.GetTvSeries(id);
            return tvSeries == null ? (IHttpActionResult)NotFound() : Ok(tvSeries);
        }

        [HttpGet]
        [Route("page/{page:int:min(1)}")]
        public IHttpActionResult GetTvSerialsByPage(int page)
        {
            return Ok();
        }
    }
}