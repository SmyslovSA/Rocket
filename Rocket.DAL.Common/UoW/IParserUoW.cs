using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.Parser;
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
        IResourceRepository Resources { get; }

        /// <summary>
        /// Возвращает репозиторий настроек парсера
        /// </summary>
        IParserSettingsRepository ParserSettings { get; }

        /// <summary>
        /// Возвращает репозиторий элементов ресурса
        /// </summary>
        IResourceItemRepository ResourceItems { get; }

        /// <summary>
        /// Возвращает список настроек парсера
        /// </summary>
        /// <param name="resourceName">Название ресурса для парсинга</param>
        /// <returns>Коллекция ParserSettingsEntity</returns>
        ICollection<ParserSettingsEntity> GetParserSettingsByResourceName(string resourceName);
    }
}
