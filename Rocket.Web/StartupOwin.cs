using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(Rocket.Web.StartupOwin))]

namespace Rocket.Web
{
    public class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                EnableWelcomePage = false,
                RequireSsl = false,
                SiteName = "Rocket server",
                IssuerUri = "", //localhost... ?
                SigningCertificate = LoadCertificate(),
                Factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Load())
                .UseInMemoryScopes(MyScopes.Load())
                .UseInMemoryUsers(Users.Load())
                // .UserService() вкрутить когда будет UserService
            };


            app.UseIdentityServer(new IdentityServerOptions
            {
                RequireSsl = false,
                SiteName = "Identity Server",
                SigningCertificate = LoadCertificate(),
                EnableWelcomePage = false,
                // Factory = factory
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

    public static class Clients
    {
        public static IEnumerable<Client> Load()
        {
            return new[]
            {
                new Client()
                {
                    ClientId = "client",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("secret-rocket".Sha256())
                    },
                    ClientName = "Android",
                    AllowAccessToAllScopes = true,
                    Flow = Flows.ResourceOwner
                }
            };
        }
    }

    public static class MyScopes
    {
        public static IEnumerable<Scope> Load()
        {
            // настроить скопы согласно модельки юзера
            return new[] {StandardScopes.OpenId, StandardScopes.Profile}; 
        }
    }

    public static class Users
    {
        public static List<InMemoryUser> Load()
        {
            return new List<InMemoryUser>()
            {
                new InMemoryUser()
                {
                    Subject = "user123",
                     Username = "JohnDoe",
                     Password = "asdf"
                }
            };
        }
    }
}
