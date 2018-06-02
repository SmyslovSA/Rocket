using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Rocket.DAL.IdentityModule
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var db = context.Get<CustomDbContext>();
            var manager = new AppUserManager(new UserStore<AppUser>(db));
            return manager;
        }
    }
}

// todo startup.cs => app.CreatePerOwinContext(CustomUserManager.Create);

//using Microsoft.Owin;
//using Owin;
//using AspNetIdentityApp.Models;
//using Microsoft.Owin.Security.Cookies;
//using Microsoft.AspNet.Identity;
 
//[assembly: OwinStartup(typeof(AspNetIdentityApp.Startup))]
 
//namespace AspNetIdentityApp
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            // настраиваем контекст и менеджер
//            app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
//            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
//            app.UseCookieAuthentication(new CookieAuthenticationOptions
//            {
//                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
//                LoginPath = new PathString("/Account/Login"),
//            });
//        }
//    }
//}