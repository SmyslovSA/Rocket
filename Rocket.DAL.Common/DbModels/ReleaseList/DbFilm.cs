using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rocket.DAL.Common.DbModels.ReleaseList
{
    /// <summary>
    /// Представляет модель хранения данных о фильмах
    /// </summary>
    public class DbFilm : DbBaseRelease
    {
        /// <summary>
        /// Возвращает или задает относительный путь
        /// от корневой папки приложения к файлу изображения постера фильма
        /// </summary>
        public string PosterImagePath { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию режиссеров, которые сняли фильм
        /// </summary>
        public virtual ICollection<DbPerson> Directors { get; set; } = new Collection<DbPerson>();

        /// <summary>
        /// Возвращает или задает коллекцию актёров, которые снялись в фильме
        /// </summary>
        public virtual ICollection<DbPerson> Cast { get; set; } = new Collection<DbPerson>();

        /// <summary>
        /// Возвращает или задает коллекцию жанров, к которым относится фильм
        /// </summary>
        public virtual ICollection<DbVideoGenre> Genres { get; set; } = new Collection<DbVideoGenre>();

        /// <summary>
        /// Возвращает или задает коллекцию стран, которые участвовали в создании фильма
        /// </summary>
        public virtual ICollection<DbCountry> Countries { get; set; } = new Collection<DbCountry>();

        /// <summary>
        /// Возвращает или задает продолжительность фильма
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Возвращает или задает краткое описание фильма
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Возвращает или задает ссылку на трейлер фильма
        /// </summary>
        public string TrailerLink { get; set; }
    }
}