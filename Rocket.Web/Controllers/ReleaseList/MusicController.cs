using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Services.ReleaseList;

namespace Rocket.Web.Controllers.ReleaseList
{
	[RoutePrefix("music")]
	public class MusicController : ApiController
    {
	    private IMusicDetailedInfoService _musicDetailedInfoService;

	    public MusicController(IMusicDetailedInfoService musicDetailedInfoService)
	    {
		    _musicDetailedInfoService = musicDetailedInfoService;
	    }

	    [HttpGet]
	    [Route("{id:int:min(1)}")]
	    public IHttpActionResult GetMusicById(int id)
	    {
		    var model = _musicDetailedInfoService.GetMusic(id);
		    _musicDetailedInfoService.Dispose();
		    return model == null ? (IHttpActionResult)NotFound() : Ok(model);
	    }
	}
}
