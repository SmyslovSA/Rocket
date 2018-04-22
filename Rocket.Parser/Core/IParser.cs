using AngleSharp.Dom.Html;

namespace Rocket.Parser.Core
{
    /// <summary>
    /// Парсер
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IParser<T> where T : class
    {
        /// <summary>
        /// Парсинг
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        T Parse(IHtmlDocument document);
    }
}
