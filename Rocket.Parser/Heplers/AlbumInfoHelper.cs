using System.Collections.Specialized;
using System.Configuration;

namespace Rocket.Parser.Heplers
{
    internal static class AlbumInfoHelper
    {

        private static readonly NameValueCollection RunSettings;
        private static readonly NameValueCollection Urls;

        private const string ParseIsSwitchOnKey = "ParseIsSwitchOn";
        private const string ParsePeriodInMinutesKey = "ParsePeriodInMinutes";
        private const string ParseBaseUrlKey = "ParseBaseUrl";

        static AlbumInfoHelper()
        {
            RunSettings = (NameValueCollection)ConfigurationManager.GetSection(
                ProjectNameConstants.AlbumInfoSectionGroupName + "/" + ProjectNameConstants.RunSettingsSectionName);

            Urls = (NameValueCollection)ConfigurationManager.GetSection(
                ProjectNameConstants.AlbumInfoSectionGroupName + "/" + ProjectNameConstants.UrlsSectionName);
        }

        public static string GetParseIsSwitchOn()
        {
            return RunSettings.Get(ParseIsSwitchOnKey);
        }

        public static string GetParsePeriodInMinutes()
        {
            return RunSettings.Get(ParsePeriodInMinutesKey);
        }

        public static string GetBaseUrl()
        {
            return Urls.Get(ParseBaseUrlKey);
        }
    }
}
