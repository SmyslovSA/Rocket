namespace Rocket.Parser.Core
{
    /// <summary>
    /// Настройки парсера
    /// </summary>
    public interface IParserSettings
    {
        /// <summary>
        /// URL сата
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Префикс пути конкретной страницы
        /// </summary>
        string Prefix { get; set; }

        /// <summary>
        /// Номер первой страницы
        /// </summary>
        int StartPoint { get; set; }

        /// <summary>
        /// Номер конечной страницы
        /// </summary>
        int EndPoint { get; set; }
    }
}
