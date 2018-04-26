using Ninject.Modules;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Parsers;
using Rocket.Parser.Services;

namespace Rocket.Parser.Config
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoadHtmlService>().To<LoadHtmlService>();
            Bind<IParseAlbumInfoService>().To<ParseAlbumInfoService>();
            Bind<IAlbumInfoParser>().To<AlbumInfoParser>();
        }
        
    }
}
