using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace Rocket.Web.Owin
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Load()
        {
            // настроить скопы согласно модельки юзера
            return new[] { StandardScopes.OpenId, StandardScopes.Profile };
        }
    }
}