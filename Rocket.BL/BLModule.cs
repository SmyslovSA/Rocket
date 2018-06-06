﻿using FluentValidation;
using Ninject.Modules;
using Rocket.BL.Common.Services;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.BL.Common.Services.ReleaseList;
using Rocket.BL.Services;
using Rocket.BL.Services.PersonalArea;
using Rocket.BL.Services.ReleaseList;
using Rocket.BL.Validators.User;

namespace Rocket.BL
{
    public class BLModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITvSeriesDetailedInfoService>().To<TvSeriesDetailedInfoService>();
            Bind<IMusicDetailedInfoService>().To<MusicDetailedInfoService>();
            Bind<IEpisodeService>().To<EpisodeService>();
            Bind<IPersonalData>().To<PersonalDataService>();
            Bind<IValidator<Common.Models.User.User>>().To<UserValidatorCheckRequiredFields>();
            Bind<IEmailManager>().To<ChangeEmailManagerService>();
            Bind<IUserPaymentService>().To<UserPaymentService>();
            Bind<IGenreManager>().To<ChangeGenreManagerService>();
            Bind<IGenreService>().To<GenreService>();
            Bind<IUserAccountLevelService>().To<UserAccountLevelService>();
        }
    }
}