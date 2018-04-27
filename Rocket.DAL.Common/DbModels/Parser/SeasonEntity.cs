using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.DAL.Common.DbModels.Parser
{
    /// <summary>
    /// Сущность модели сезона сериала.
    /// </summary>
    public class SeasonEntity
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер сезона.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Url изображения постера сезона.
        /// </summary>
        public string PosterImageUrl { get; set; }

        /// <summary>
        /// Краткое описание сезона.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Список серий сезона.
        /// </summary>
        public ICollection<DbEpisode> Episodes { get; set; }

        /// <summary>
        /// Id ериала к которому относится этот сезон.
        /// </summary>
        public int DbTvSeriesId { get; set; }

        /// <summary>
        /// Возвращает или задает сериал,
        /// к которому относится этот сезон
        /// </summary>
        public DbTVSeries DbTvSeries { get; set; }
    }
}
