using System.Data.Entity;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.DAL.Common.Context
{
    /// <summary>
    /// Контекст данных Rocket
    /// </summary>
    public interface IRocketContext : IDbContext
    {
        /// <summary>
        /// DbSet ресурсов для парсинга
        /// </summary>
        DbSet<ResourceEntity> Resources { get; set; }

        /// <summary>
        /// DbSet настроек парсера
        /// </summary>
        DbSet<ParserSettingsEntity> ParserSettings { get; set; }

        /// <summary>
        /// DbSet элемента ресурса
        /// </summary>
        DbSet<ResourceItemEntity> ResourceItems { get; set; }
    }
}
