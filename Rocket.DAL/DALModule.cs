﻿using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using Ninject.Web.Common;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.DbModels.Notification;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.IDbUserRoleRepository;
using Rocket.DAL.Common.Repositories.Notification;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Context;
using Rocket.DAL.Identity;
using Rocket.DAL.Repositories;
using Rocket.DAL.Repositories.Notification;
using Rocket.DAL.Repositories.PersonalArea;
using Rocket.DAL.Repositories.User;
using Rocket.DAL.Repositories.UserRole;
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
            Bind<RocketContext>().ToSelf().InRequestScope();
            //Bind<DbContext>().To<RocketContext>().InRequestScope();
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
            Bind<IBaseRepository<DbUserPayment>>().To<BaseRepository<DbUserPayment>>();
            Bind<IDbEmailRepository>().To<DbEmailRepository>();
            Bind<IDbUserRepository>().To<DbUserRepository>();
            Bind<IDbRoleRepository>().To<DbRoleRepository>();
            Bind<IDbPermissionRepository>().To<DbPermissionRepository>();
            Bind<IDbAuthorisedUserRepository>().To<DbAuthorisedUserRepository>();
            Bind<IBaseRepository<NotificationsLogEntity>>().To<BaseRepository<NotificationsLogEntity>>();
            Bind<IDbEmailTemplateRepository>().To<DbEmailTemplateRepository>();
            Bind<IDbGuestBillingMessageRepository>().To<DbGuestBillingMessageRepository>();
            Bind<IDbReceiverRepository>().To<DbReceiverRepository>();
            Bind<IDbReleaseMessageRepository>().To<DbReleaseMessageRepository>();
            Bind<IDbUserBillingMessageRepository>().To<DbUserBillingMessageRepository>();
            Bind<IDbCustomMessageRepository>().To<DbCustomMessageRepository>();

            Bind<RocketUserManager>().ToSelf().InRequestScope();
            Bind<IUserStore<DbUser>>().ToMethod(ctx => new UserStore<DbUser>(new RocketContext()));
            //Bind<IUserStore<DbUser, int>>()
            //    .ToConstructor(context => new UserStore<DbUser, DbRole, int, DbUserLogin, DbUserRole, DbUserClaim>(context.Inject<DbContext>()))
            //    .InRequestScope();
            //Bind<UserManager<DbUser, int>>().ToSelf().InRequestScope();
            //Bind<IUserService>()
            //    .ToConstructor(context => new AspNetIdentityUserService<DbUser, int>(context.Inject<UserManager<DbUser, int>>(), null))
            //    .InRequestScope();

            //UoW
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
