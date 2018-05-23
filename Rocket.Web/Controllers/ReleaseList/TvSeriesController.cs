using Rocket.BL.Common.Services.ReleaseList;
using System.Web.Http;
using Rocket.Web.ConfigHandlers;

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

        /// <summary>
        /// Возвращает информацию о сериале с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сериал</returns>
        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult GetTvSeriesById(int id)
        {
            var tvSeries = _tvSeriesDetailedInfoService.GetTvSeries(id);
            return tvSeries == null ? (IHttpActionResult)NotFound() : Ok(tvSeries);
        }

        /// <summary>
        /// Возвращает страницу рейтинговыых сериалов с заданным номером страницы
        /// </summary>
        /// <param name="pageNumber">Номер страницы</param>
        /// <returns>Страница рейтинговых сериалов</returns>
        [HttpGet]
        [Route("page/{pageNumber:int:min(1)}")]
        public IHttpActionResult GetTvSerialsByPage(int pageNumber)
        {
            var page = _tvSeriesDetailedInfoService.GetPageInfoByRating(
                SettingsManager.ReleasesSettings.Pagination.PageSize,
                pageNumber);
            return pageNumber <= page.TotalPagesCount ? Ok(page) : (IHttpActionResult)NotFound();
        }
    }
}