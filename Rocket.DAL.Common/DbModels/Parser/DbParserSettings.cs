﻿namespace Rocket.DAL.Common.DbModels.Parser
{
    /// <summary>
    /// Параметры парсера
    /// </summary>
    public class DbParserSettings
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
        /// URL сата
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Префикс пути конкретной страницы
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Номер первой страницы
        /// </summary>
        public int StartPoint { get; set; }

        /// <summary>
        /// Номер конечной страницы
        /// </summary>
        public int EndPoint { get; set; }
    }
}
