using Rocket.DAL.Common.Repositories;

namespace Rocket.DAL.Common.UoW
{
    /// <summary>
    /// Представляет unit of work для сущностей парсера
    /// </summary>
    public interface IParserUoW : IUnitOfWork
    {
        /// <summary>
        /// Возвращает репозиторий ресурсов для парсинга
        /// </summary>
        IResourceEntityRepository ResourceEntities { get; }
    }
}
