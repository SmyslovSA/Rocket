using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Rocket.Web.Startup))]

namespace Rocket.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // This server will be accessed by clients from other domains, so
            //  we open up CORS. This needs to be before the call to
            //  .MapSignalR()!
            //
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.MapSignalR();
        }
    }
}