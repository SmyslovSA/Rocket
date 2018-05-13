using System.Collections.Specialized;
using System.Configuration;

namespace Rocket.Parser.Heplers
{
    internal static class AlbumInfoHelper
    {
        public const string ReleaseNamePattern = @"(?<=\- )(?: ?+[^\(])++";
        public const string ReleaseTypePattern = @"(?<=\()\w++";
        public const string ReleaseGenrePattern = @"\w[^,]*+";
        public const string ReleaseTrackListPattern = @"\w(\d*+[^\.])++";

        public const string LongDateFormat = "d MMMM yyyy г.";
        public const string ShortDateFormat = "yyyy";

        public const string CoversPath = @"c:\tmp\MusicCovers\";

        private static readonly NameValueCollection ResourceSettings;

        //настройки ресурса
        private const string AlbumInfoResourceKey = "AlbumInfoResource";

        static AlbumInfoHelper()
        {
            ResourceSettings = (NameValueCollection)ConfigurationManager.GetSection(
                ProjectNameConstants.AlbumInfoSectionGroupName + "/" + 
                ProjectNameConstants.ResourceSettingsSectionName);
        }

        public static string GetAlbumInfoResource()
        {
            return ResourceSettings.Get(AlbumInfoResourceKey);
        }
    }
}
