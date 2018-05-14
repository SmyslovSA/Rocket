using System;

namespace Rocket.DAL.Common.DbModels.ReleaseList
{
    /// <summary>
    /// Представляет модель хранения данных о сериях сериалов
    /// </summary>
    public class DbEpisode : DbBaseRelease
    {
        /// <summary>
        /// Возвращает или задает номер серии в сезоне
        /// </summary>
        public int Number { get; set; }
        
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