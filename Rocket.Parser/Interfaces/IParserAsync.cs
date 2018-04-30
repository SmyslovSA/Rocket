using System.Threading.Tasks;

namespace Rocket.Parser.Interfaces
{
    /// <summary>
    /// Базовый интерфейс парсера
    /// </summary>
    internal interface IParserAsync
    {
        /// <summary>
        /// Запуск процесса парсинга
        /// </summary>
        /// <returns></returns>
        Task ParseAsync();
    }
}
