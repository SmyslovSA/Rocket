using System;

namespace Rocket.DAL.Common.DbModels.Parser
{
    /// <summary>
    /// Элемент ресурса
    /// </summary>
    public class ResourceItemEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id типа ресурса
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Id внутри ресурса
        /// </summary>
        public string ResourceInternalId { get; set; }

        /// <summary>
        /// Ссылка на страницу элемента ресурса
        /// </summary>
        public string ResourceItemLink { get; set; }

        /// <summary>
        /// Дата и веремя создания
        /// </summary>
        public DateTime CreatedDateTime { get; } //todo default value 

        /// <summary>
        /// Дата и веремя последней обработки
        /// </summary>
        public DateTime LastModified { get; } //todo add trigger on table

        /// <summary>
        /// Ссылка на ресурс
        /// </summary>
        public ResourceEntity Resource { get; set; }

    }
}
