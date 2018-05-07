using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.ReleaseList
{
    /// <summary>
    /// Представляет модель хранения данных о сериалах
    /// </summary>
    public class DbTVSeries
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
        /// от корневой папки приложения к файлу изображения постера сериала
        /// </summary>
        public string PosterImagePath { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию режиссеров, которые сняли сериал
        /// </summary>
        public ICollection<DbPerson> Directors { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию актёров, которые снялись в сериале
        /// </summary>
        public ICollection<DbPerson> Cast { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию жанров, к которым относится сериал
        /// </summary>
        public ICollection<DbVideoGenre> Genres { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию стран, которые участвовали в создании сериала
        /// </summary>
        public ICollection<DbCountry> Countries { get; set; }

        /// <summary>
        /// Возвращает или задает краткое описание сериала
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сезонов сериала
        /// </summary>
        public ICollection<DbSeason> DbSeasons { get; set; }
    }
}