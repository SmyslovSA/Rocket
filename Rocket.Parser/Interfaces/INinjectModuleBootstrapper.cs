using System.Collections.Generic;
using Ninject.Modules;

namespace Rocket.Parser.Interfaces
{
    public interface INinjectModuleBootstrapper
    {
        IList<INinjectModule> GetModules();
    }
}
