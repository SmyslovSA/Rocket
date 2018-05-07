using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rocket.DAL.Common.DbModels.ReleaseList
{
    /// <summary>
    /// Представляет модель хранения данных о сезонах сериалов
    /// </summary>
    public class DbSeason
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор сезона
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает порядковый номер сезона
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Возвращает или задает относительный путь
        /// от корневой папки приложения к файлу изображения постера сезона
        /// </summary>
        public string PosterImagePath { get; set; }

        /// <summary>
        /// Возвращает или задает краткое описание сезона
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию серий сезона
        /// </summary>
        public virtual ICollection<DbEpisode> Episodes { get; set; } = new Collection<DbEpisode>();

        /// <summary>
        /// Возвращает или задает идентификатор сериала,
        /// к которому относится этот сезон
        /// </summary>
        public int DbTVSeriesId { get; set; }

        /// <summary>
        /// Возвращает или задает сериал,
        /// к которому относится этот сезон
        /// </summary>
        public DbTVSeries DbTVSeries { get; set; }
    }
}