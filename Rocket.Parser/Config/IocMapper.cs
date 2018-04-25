using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Rocket.DAL.Common.Repositories;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Services;

namespace Rocket.Parser.Config
{
    public static class IocMapper
    {
        public class NinjectRegistrations : NinjectModule
        {
            public override void Load()
            {
                Bind<ILoadHtmlService>().To<LoadHtmlService>();
                //Bind<>().To<LoadHtmlService>();
            }
        }
    }
}
