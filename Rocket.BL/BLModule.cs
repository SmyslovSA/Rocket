using Ninject.Modules;
using Rocket.BL.Common.Services.ReleaseList;
using Rocket.BL.Services.ReleaseList;

namespace Rocket.BL
{
    public class BLModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITvSeriesDetailedInfoService>().To<TvSeriesDetailedInfoService>();
        }
    }
}