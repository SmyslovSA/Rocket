﻿namespace Rocket.Parser.Models
{
    /// <summary>
    /// Содержит информацию о ресурсе для парсинга
    /// </summary>
    public class ResourceType
    {
        /// <summary>
        /// Id ресурса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название ресурса
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на страницу ресурса
        /// </summary>
        public string ResourceLink { get; set; }

    }
}
