using Ninject;
using Ninject.Web.Common.WebHost;
using Rocket.Web.Attribute;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rocket.Web
{
    public class Global : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapperConfig.Initialize();
            GlobalFilters.Filters.Add(new RoleAuthorizeAttribyte());
            LoggerConfig.Configure();
        }



        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(new[] {"Rocket.BL*", "Rocket.DAL*"});

            kernel.Load<WebModule>();

            return kernel;
        }
    }
}