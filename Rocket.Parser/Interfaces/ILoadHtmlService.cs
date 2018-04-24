using System.Threading.Tasks;

namespace Rocket.Parser.Interfaces
{
    /// <summary>
    /// Загружает Html
    /// </summary>
    public interface ILoadHtmlService
    {
        /// <summary>
        /// Загружает Html
        /// </summary>
        /// <param name="id">Префикс</param>
        /// <param name="url">URL</param>
        /// <returns>Html в виде строки</returns>
        Task<string> GetSourceById(string id, string url);
    }
}