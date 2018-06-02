using System;
using System.Collections.Generic;
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
                SigningCertificate = LoadCert(),
                Factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Load())
                .UseInMemoryScopes(MyScopes.Load())
                .UseInMemoryUsers(Users.Load())
                // .UserService() вкрутить когда будет UserService
            };


            app.UseIdentityServer(options);

            // fish - тут должно быть обновление 
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions());

        }

        private X509Certificate2 LoadCert()
        {
            throw new NotImplementedException(); // download demo sertificate?
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
            return new[] {StandardScopes.OpenId, StandardScopes.Profile}; // настроить скопы согласно модельки юзера
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
                    // UserName = "JohnDoe",
                    // pass = "asdf"
                }
            };
        }
    }
}
