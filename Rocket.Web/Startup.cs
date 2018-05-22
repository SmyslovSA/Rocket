using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(Rocket.Web.Startup))]

namespace Rocket.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}