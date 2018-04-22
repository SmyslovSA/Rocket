using System.Collections.Generic;

namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Представляет информацию о конкретном сериале
    /// </summary>
    public class TVSeries
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор сериала
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает название сериала
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает относительный путь
        /// от корневой папки приложения к файлу изображения постера фильма
        /// </summary>
        public string PosterImagePath { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию режиссеров, которые сняли сериал
        /// </summary>
        public ICollection<Person> Directors { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию актёров, которые снялись в сериале
        /// </summary>
        public ICollection<Person> Cast { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию жанров, к которым относится сериал
        /// </summary>
        public ICollection<VideoGenre> Genres { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию стран, которые участвовали в создании сериала
        /// </summary>
        public ICollection<string> Countries { get; set; }

        /// <summary>
        /// Возвращает или задает краткое описание сериала
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сезонов сериала
        /// </summary>
        public ICollection<Season> Seasons { get; set; }
    }
}
