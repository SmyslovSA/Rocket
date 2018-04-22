using System.Collections.Generic;

namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Представляет информацию о конкретном сезоне сериала
    /// </summary>
    public class Season
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
        public ICollection<Episode> Episodes { get; set; }
    }
}