using System.Threading.Tasks;

namespace Rocket.Parser.Interfaces
{
    /// <summary>
    /// Базовый интерфейс парсера
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Запуск процесса парсинга
        /// </summary>
        /// <returns></returns>
        Task<int> ParseAsync();
    }
}
