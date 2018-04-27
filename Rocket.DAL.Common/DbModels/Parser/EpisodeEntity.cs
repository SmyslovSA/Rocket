using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.DAL.Common.DbModels.Parser
{
    /// <summary>
    /// Сущность модели серии.
    /// </summary>
    public class EpisodeEntity
    {

        /// <summary>
        /// Уникальный идентификатор серии.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата выхода серии(рус).
        /// </summary>
        public DateTime ReleaseDateRu { get; set; }

        /// <summary>
        /// Дата выхода серии(англ).
        /// </summary>
        public DateTime ReleaseDateEn { get; set; }

        /// <summary>
        /// Номер серии в сезоне.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Название серии.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Продолжительность серии.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Краткое описание серии.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Id сезона к которому относится эта серия.
        /// </summary>
        public int DbSeasonId { get; set; }

        /// <summary>
        /// Сезон к которому относится эта серия.
        /// </summary>
        public DbSeason DbSeason { get; set; }
    }
}
