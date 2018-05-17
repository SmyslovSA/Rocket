using Ninject.Modules;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
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
            Bind<IRepository<CategoryEntity>>().To<Repository<CategoryEntity>>();
            Bind<IRepository<EpisodeEntity>>().To<Repository<EpisodeEntity>>();
            Bind<IRepository<GenreEntity>>().To<Repository<GenreEntity>>();
            Bind<IRepository<PersonEntity>>().To<Repository<PersonEntity>>();
            Bind<IRepository<PersonTypeEntity>>().To<Repository<PersonTypeEntity>>();
            Bind<IRepository<SeasonEntity>>().To<Repository<SeasonEntity>>();
            Bind<IRepository<TvSeriasEntity>>().To<Repository<TvSeriasEntity>>();

            //UoW
            Bind<IUnitOfWorkP>().To<UnitOfWork>().InSingletonScope();
        }
    }
}