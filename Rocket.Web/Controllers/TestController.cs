using System.Web.Http;
using System.Web.Http.Cors;

namespace Rocket.Web.Controllers
{
    [RoutePrefix("api/test")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Json("GET: Test message");
        }
    }
}