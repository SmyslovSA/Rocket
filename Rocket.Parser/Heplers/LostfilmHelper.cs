using System.Collections.Specialized;
using System.Configuration;

namespace Rocket.Parser.Heplers
{
    internal static class LostfilmHelper
    {
        private static readonly NameValueCollection RunSettings;
        private static readonly NameValueCollection Urls;
        private static readonly NameValueCollection TvSerailListHeaderSelectors;

        private const string ParseIsSwitchOnKey = "ParseIsSwitchOn";
        private const string ParsePeriodInMinutesKey = "ParsePeriodInMinutes";
        private const string ParseBaseUrlKey = "ParseBaseUrl";
        private const string AdditionalUrlToSerialListKey = "AdditionalUrlToSerialList";

        private const string TvSerialHeaderKeywordStatusKey = "KeywordStatus";
        private const string TvSerialHeaderKeywordYearStartKey = "KeywordYearStart";
        private const string TvSerialHeaderKeywordCanalKey = "KeywordCanal";
        private const string TvSerialHeaderKeywordGenreKey = "KeywordGenre";
        private const string TvSerailListHeaderBaseKey = "Base";
        private const string TvSerialHeaderKey = "TvSerial";
        private const string TvSerialHeaderDetailKey = "TvSerialDetail";
        private const string TvSerialHeaderLostfilmRateKey = "TvSerialLostfilmRate";
        private const string TvSerialHeaderDetailImageUrlThumbKey = "TvSerialDetailImageUrlThumb";
        private const string TvSerialHeaderDetailTvSerialNameRuKey = "TvSerialDetailTvSerialNameRu";
        private const string TvSerialHeaderDetailTvSerialNameEnKey = "TvSerialDetailTvSerialNameEn";
        private const string TvSerialHeaderDetailPaneKey = "TvSerialDetailPane";

        static LostfilmHelper()
        {
            RunSettings =(NameValueCollection)ConfigurationManager.GetSection(
                    SectionGroupHelper.LostfilmSectionGroupName + "/" + SectionGroupHelper.RunSettingsSectionName);

            Urls = (NameValueCollection)ConfigurationManager.GetSection(
                SectionGroupHelper.LostfilmSectionGroupName + "/" + SectionGroupHelper.UrlsSectionName);

            TvSerailListHeaderSelectors = (NameValueCollection)ConfigurationManager.GetSection(
                SectionGroupHelper.LostfilmSectionGroupName + "/" + 
                SectionGroupHelper.TvSerailListHeaderSelectorsSectionName);
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

        public static string GetAdditionalUrlToSerialList()
        {
            return Urls.Get(AdditionalUrlToSerialListKey);
        }

        public static string GetTvSerailListHeaderBase()
        {
            return TvSerailListHeaderSelectors.Get(TvSerailListHeaderBaseKey);
        }

        public static string GetTvSerialHeader()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderKey);
        }

        public static string GetTvSerialHeaderDetail()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderDetailKey);
        }

        public static string GetTvSerialHeaderLostfilmRate()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderLostfilmRateKey);
        }

        public static string GetTvSerialHeaderDetailImageUrlThumb()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderDetailImageUrlThumbKey);
        }

        public static string GetTvSerialHeaderDetailTvSerialNameRu()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderDetailTvSerialNameRuKey);
        }

        public static string GetTvSerialHeaderDetailTvSerialNameEn()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderDetailTvSerialNameEnKey);
        }

        public static string GetTvSerialHeaderDetailPane()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderDetailPaneKey);
        }

        public static string GetTvSerialHeaderKeywordStatus()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderKeywordStatusKey);
        }

        internal static string GetTvSerialHeaderKeywordYearStart()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderKeywordYearStartKey);
        }

        public static string GetTvSerialHeaderKeywordCanal()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderKeywordCanalKey);
        }

        public static string GetTvSerialHeaderKeywordGenre()
        {
            return TvSerailListHeaderSelectors.Get(TvSerialHeaderKeywordGenreKey);
        }

    }
}
