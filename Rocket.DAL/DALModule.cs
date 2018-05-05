using Ninject.Modules;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Repositories;

namespace Rocket.DAL
{
    public class DALModule : NinjectModule
    {
        /// <summary>
        /// Настройка Ninject для DAL
        /// </summary>
        public override void Load()
        {
            Bind<IResourceRepository>().To<ResourceRepository>();
            Bind<IParserSettingsRepository>().To<ParserSettingsRepository>();
            Bind<IResourceItemRepository>().To<ResourceItemRepository>();

            //UoW
            //Bind<IParserUoW>().To<ParserUoW>();
        }
    }
}
