using System.Web.Http;
using Rocket.Web.Helpers;

namespace Rocket.Web.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Json("GET: Test message");
        }

        [HttpGet]
        [Route("notifyAll")]
        public IHttpActionResult NotifyAll(string msg)
        {
            PushNotificationsHelper.SendPushNotificationsToAll(msg);

            return Ok();
        }

    }
}