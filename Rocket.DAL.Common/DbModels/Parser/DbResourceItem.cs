using System;

namespace Rocket.DAL.Common.DbModels.Parser
{
    /// <summary>
    /// Элемент ресурса
    /// </summary>
    public class DbResourceItem
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
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// Дата и веремя последней обработки
        /// </summary>
        public DateTime LastModify { get; set; }
    }
}
