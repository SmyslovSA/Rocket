
namespace Rocket.Parser.Core.AlbumInfo
{
    /// <summary>
    /// Реализация настроек парсера для парсинга информации о наличии релизов с сайта album-info.ru
    /// </summary>
    class AlbumInfoItemSettings : IParserSettings
    {
        public AlbumInfoItemSettings(int start, int end, string baseUrl, string prefix )
        {
            StartPoint = start;
            EndPoint = end;
            BaseUrl = baseUrl;
            Prefix = prefix;
        }

        public string BaseUrl { get; set; }

        public string Prefix { get; set; }

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
    }
}
