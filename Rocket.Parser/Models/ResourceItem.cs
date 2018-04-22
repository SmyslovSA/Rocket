using System;

namespace Rocket.Parser.Models
{
    /// <summary>
    /// Элемент ресурса
    /// </summary>
    public class ResourceItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id типа ресурса
        /// </summary>
        public int ResourceTypeId { get; set; }

        /// <summary>
        /// Id внутри ресурса
        /// </summary>
        public string ResourceId { get; set; }

        /// <summary>
        /// Ссылка на страницу элемента ресурса
        /// </summary>
        public string ResourceItemLink { get; set; }

        /// <summary>
        /// Дата и веремя создания
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// Дата и веремя последней обработки
        /// </summary>
        public DateTime LastModify { get; set; }
    }
}
