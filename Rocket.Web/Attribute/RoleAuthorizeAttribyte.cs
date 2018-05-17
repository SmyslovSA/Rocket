using System.Web;

namespace Rocket.Web.Attribute
{
    public class RoleAuthorizeAttribyte : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
            // httpContext.User - по юзеру олучить пермишены ( сервис из конструктора ??))
        }
    }
}