using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common.WebHost;

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