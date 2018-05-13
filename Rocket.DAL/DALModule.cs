using Ninject.Modules;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.DbModels.Parser;
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
            //контекст
            Bind<RocketContext>().ToMethod(ctx => new RocketContext()).InSingletonScope();

            //репозитарии
            Bind<IRepository<ResourceEntity>>().To<Repository<ResourceEntity>>();
            Bind<IRepository<ParserSettingsEntity>>().To<Repository<ParserSettingsEntity>>();
            Bind<IRepository<ResourceItemEntity>>().To<Repository<ResourceItemEntity>>();
            Bind<IRepository<DbMusic>>().To<Repository<DbMusic>>();
            Bind<IRepository<DbMusicGenre>>().To<Repository<DbMusicGenre>>();
            Bind<IRepository<DbMusicTrack>>().To<Repository<DbMusicTrack>>();
            Bind<IRepository<DbMusician>>().To<Repository<DbMusician>>();

            //UoW
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}
