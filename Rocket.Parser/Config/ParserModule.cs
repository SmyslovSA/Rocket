using Ninject.Modules;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Parsers;
using Rocket.Parser.Services;

namespace Rocket.Parser.Config
{
    public class ParserModule : NinjectModule
    {
        /// <summary>
        /// Настройка Ninject для парсера
        /// </summary>
        public override void Load()
        {
            Bind<ILoadHtmlService>().To<LoadHtmlService>();
            Bind<IParseAlbumInfoService>().To<ParseAlbumInfoService>();
            Bind<IAlbumInfoParser>().To<AlbumInfoParser>();
            Bind<ILostfilmParseService>().To<LostfilmParseService>();
        }
    }
}
