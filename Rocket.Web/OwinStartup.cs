using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using Owin;
using Rocket.Web.Owin;

//[assembly: OwinStartup(typeof(Rocket.Web.StartupOwin))]

namespace Rocket.Web
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.MapSignalR();

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Load())
                .UseInMemoryScopes(Scopes.Load())
                .UseInMemoryUsers(Users.Load());
                // .UserService() вкрутить когда будет UserService

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
}
