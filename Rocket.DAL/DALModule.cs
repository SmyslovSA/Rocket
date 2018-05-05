using Ninject.Modules;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories.Temp;
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
            //контекст
            Bind<RocketContext>().ToMethod(ctx => new RocketContext()).InSingletonScope();

            //репозитарии
            Bind<IRepository<ResourceEntity>>().To<Repository<ResourceEntity>>();
            Bind<IRepository<ParserSettingsEntity>>().To<Repository<ParserSettingsEntity>>();
            Bind<IRepository<ResourceItemEntity>>().To<Repository<ResourceItemEntity>>();

            //UoW
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}
