using System.Collections.Generic;
using Ninject.Modules;
using Rocket.DAL;
using Rocket.Parser.Config;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Bootstrapper
{
    public class DataAccessBootstrapper : INinjectModuleBootstrapper
    {
        public IList<INinjectModule> GetModules()
        {
            return new List<INinjectModule>()
            {
                new DALModule(),
                new ParserModule()
            };
        }
    }
}
