using System;
using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения данных о сериях сериалов
    /// </summary>
    public class DbEpisode
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор серии
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает информацию о выходе серии
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Возвращает или задает номер серии в сезоне
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Возвращает или задает название серии
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает продолжительность серии
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Возвращает или задает краткое описание серии
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Возвращает или задает идентификатор сезона,
        /// к которому относится эта серия
        /// </summary>
        public int DbSeasonId { get; set; }

        /// <summary>
        /// Возвращает или задает сезон,
        /// к которому относится эта серия
        /// </summary>
        public DbSeason DbSeason { get; set; }
    }
}