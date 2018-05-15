using Ninject;
using Ninject.Web.Common.WebHost;
using System.Web;
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
            GlobalFilters.Filters.Add(new RoleAuthorizeAttribyte());
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(new[] { "Rocket.BL*", "Rocket.DAL*" });
            return kernel;
        }
    }

    public class RoleAuthorizeAttribyte : System.Web.Mvc.AuthorizeAttribute
    {
        public RoleAuthorizeAttribyte()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
            
            // httpContext.User - по юзеру олучить пермишены ( сервис из конструктора ??))
        }
    }
}