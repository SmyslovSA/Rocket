using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rocket.DAL.Common.DbModels.User;

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
        public virtual ICollection<DbPerson> Directors { get; set; } = new Collection<DbPerson>();

        /// <summary>
        /// Возвращает или задает коллекцию актёров, которые снялись в сериале
        /// </summary>
        public virtual ICollection<DbPerson> Cast { get; set; } = new Collection<DbPerson>();

        /// <summary>
        /// Возвращает или задает коллекцию жанров, к которым относится сериал
        /// </summary>
        public virtual ICollection<DbVideoGenre> Genres { get; set; } = new Collection<DbVideoGenre>();

        /// <summary>
        /// Возвращает или задает коллекцию стран, которые участвовали в создании сериала
        /// </summary>
        public virtual ICollection<DbCountry> Countries { get; set; } = new Collection<DbCountry>();

        /// <summary>
        /// Возвращает или задает краткое описание сериала
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сезонов сериала
        /// </summary>
        public virtual ICollection<DbSeason> DbSeasons { get; set; } = new Collection<DbSeason>();
    }
}