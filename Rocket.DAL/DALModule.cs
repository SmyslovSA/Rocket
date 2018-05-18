﻿using Ninject.Modules;
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
            Bind<IBaseRepository<ResourceEntity>>().To<BaseRepository<ResourceEntity>>();
            Bind<IBaseRepository<ParserSettingsEntity>>().To<BaseRepository<ParserSettingsEntity>>();
            Bind<IBaseRepository<ResourceItemEntity>>().To<BaseRepository<ResourceItemEntity>>();
            Bind<IBaseRepository<DbMusic>>().To<BaseRepository<DbMusic>>();
            Bind<IBaseRepository<DbMusicGenre>>().To<BaseRepository<DbMusicGenre>>();
            Bind<IBaseRepository<DbMusicTrack>>().To<BaseRepository<DbMusicTrack>>();
            Bind<IBaseRepository<DbMusician>>().To<BaseRepository<DbMusician>>();
            Bind<IBaseRepository<CategoryEntity>>().To<BaseRepository<CategoryEntity>>();
            Bind<IBaseRepository<EpisodeEntity>>().To<BaseRepository<EpisodeEntity>>();
            Bind<IBaseRepository<GenreEntity>>().To<BaseRepository<GenreEntity>>();
            Bind<IBaseRepository<PersonEntity>>().To<BaseRepository<PersonEntity>>();
            Bind<IBaseRepository<PersonTypeEntity>>().To<BaseRepository<PersonTypeEntity>>();
            Bind<IBaseRepository<SeasonEntity>>().To<BaseRepository<SeasonEntity>>();
            Bind<IBaseRepository<TvSeriasEntity>>().To<BaseRepository<TvSeriasEntity>>();

            //UoW
            Bind<IUnitOfWorkP>().To<UnitOfWork>().InSingletonScope();
        }
    }
}