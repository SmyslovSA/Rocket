using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения данных о жанрах фильмов и сериалов
    /// </summary>
    public class DbVideoGenre
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор жанра
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает название жанра
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию фильмов,
        /// которые относятся к этому жанру
        /// </summary>
        public ICollection<DbFilm> DbFilms { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сериалов,
        /// которые относятся к этому жанру
        /// </summary>
        public ICollection<DbTVSeries> DbTVSerials { get; set; }
    }
}