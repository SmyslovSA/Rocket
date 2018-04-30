using Ninject.Modules;
using Rocket.DAL.Common.Context;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Context;
using Rocket.DAL.Repositories;
using Rocket.DAL.UoW;

namespace Rocket.DAL
{
    public class DALModule : NinjectModule
    {
        /// <summary>
        /// Настройка Ninject для DAL
        /// </summary>
        public override void Load()
        {
            Bind<IRocketContext>().To<RocketContext>();
            Bind<IResourceEntityRepository>().To<ResourceEntityRepository>();
            Bind<IParserUoW>().To<ParserUoW>();
        }
    }
}
