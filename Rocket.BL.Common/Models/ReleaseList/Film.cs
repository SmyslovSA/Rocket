using System;
using System.Collections.Generic;

namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Представляет информацию о конкретном фильме
    /// </summary>
    public class User
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
        /// Возвращает или задает относительный путь
        /// от корневой папки приложения к файлу изображения постера фильма
        /// </summary>
        public string PosterImagePath { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию режиссеров, которые сняли фильм
        /// </summary>
        public ICollection<Person> Directors { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию актёров, которые снялись в фильме
        /// </summary>
        public ICollection<Person> Cast { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию жанров, к которым относится фильм
        /// </summary>
        public ICollection<VideoGenre> Genres { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию стран, которые участвовали в создании фильма
        /// </summary>
        public ICollection<Country> Countries { get; set; }

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