using System.Web;

namespace Rocket.Web.Attribute
{
    public class RoleAuthorizeAttribyte : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // todo MP httpContext.User - по юзеру получить пермишены ( сервис из конструктора ??))
            // return base.AuthorizeCore(httpContext);

            return true;
        }
    }
}