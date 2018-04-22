using System;
using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения данных о фильмах
    /// </summary>
    public class DbFilm
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор фильма
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Возвращает или задает дату выхода фильма
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Возвращает или задает название фильма
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает путь к изображению постера фильма
        /// </summary>
        public string PosterImagePath { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию режиссеров, которые сняли фильм
        /// </summary>
        public ICollection<DbPerson> Directors { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию актёров, которые снялись в фильме
        /// </summary>
        public ICollection<DbPerson> Cast { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию жанров, к которым относится фильм
        /// </summary>
        public ICollection<DbVideoGenre> Genres { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию стран, которые участвовали в создании фильма
        /// </summary>
        public ICollection<DbCountry> Countries { get; set; }

        /// <summary>
        /// Возвращает или задает продолжительность фильма
        /// </summary>
        public TimeSpan Duration { get; set; }

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