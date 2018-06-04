using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Ninject;
using Owin;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Identity;
using Rocket.Web.Owin;

[assembly: OwinStartup(typeof(Rocket.Web.OwinStartup))]

namespace Rocket.Web
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.MapSignalR();

            var factory =
                new IdentityServerServiceFactory
                {
                    UserService = new Registration<IUserService>(DependencyResolver.Current.GetService<IUserService>())
                }
                .UseInMemoryClients(Clients.Load())
                .UseInMemoryScopes(Scopes.Load())
                /*.UseInMemoryUsers(Users.Load())*/;

            factory.Register(new Registration<UserManager<DbUser, string>>());

            app.UseIdentityServer(new IdentityServerOptions
            {
                RequireSsl = false,
                SiteName = "Identity Server",
                SigningCertificate = LoadCertificate(),
                EnableWelcomePage = false,
                Factory = factory
            });

            var opt = new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:2383", // ?
                RequiredScopes = new[] { "openid" },
                IssuerName = "http://localhost:2383", // ?
                SigningCertificate = LoadCertificate(),
                ValidationMode = ValidationMode.ValidationEndpoint
            };

            app.UseIdentityServerBearerTokenAuthentication(opt);
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TempRocket.cer"), "TempRocket");
        }
    }

    public class RocketIdentityService: AspNetIdentityUserService<DbUser, string>
    {
        public RocketIdentityService(UserManager<DbUser, string> userManager, Func<string, string> parseSubject = null) : base(userManager, parseSubject)
        {
        }
    }
}