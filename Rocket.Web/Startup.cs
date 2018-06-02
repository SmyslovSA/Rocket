using Owin;

//[assembly: OwinStartup(typeof(Rocket.Web.Startup))]

namespace Rocket.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // This server will be accessed by clients from other domains, so
            //  we open up CORS. This needs to be before the call to
            //  .MapSignalR()!
            // moved to OwinStartup
        }
    }
}