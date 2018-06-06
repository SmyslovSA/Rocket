﻿using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Rocket.DAL.Common.DbModels.User;
using Rocket.Web.Identity;
using Rocket.Web.Owin;

[assembly: OwinStartup(typeof(Rocket.Web.Startup))]

namespace Rocket.Web
{
    public class Startup
    {
        // 1. AppHandler
        // 2. AuthorizeAttribute
        // 3. DefaultRole   InfoLogService
        // 4. Perfomance  IUserService  ++
        // 5. Loger fileName   InfoLogService
        // 6. canActivate: [RocketAuthGuard] front
        // 7. delete Users front

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.MapSignalR();

            var factory =
                new IdentityServerServiceFactory
                {
                    UserService = new Registration<IUserService, RocketIdentityService>()
                    //new Registration<IUserService>(DependencyResolver.Current.GetService<IUserService>())
                }
                .UseInMemoryClients(Clients.Load())
                .UseInMemoryScopes(Scopes.Load())
                /*.UseInMemoryUsers(Users.Load())*/;

            factory.Register(new Registration<UserManager<DbUser, string>>());
            factory.Register(new Registration<RocketIdentityService>());

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
                Authority = "http://localhost:63613", // ?
                RequiredScopes = new[] { "openid" },
                IssuerName = "http://localhost:63613", // ?
                SigningCertificate = LoadCertificate(),
                ValidationMode = ValidationMode.ValidationEndpoint
            };

            app.UseIdentityServerBearerTokenAuthentication(opt);
        }

        private X509Certificate2 LoadCertificate()
        {
            //return new X509Certificate2(
            //    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TempRocket.cer"), "TempRocket");
            return new X509Certificate2(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\idsrv3test.pfx"), "idsrv3test");
        }
    }
}