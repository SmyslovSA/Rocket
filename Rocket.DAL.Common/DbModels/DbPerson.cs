using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения данных о человеке (режиссере, актёре или музыканте)
    /// </summary>
    public class DbPerson
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор человека
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает имя и фамилию (полное имя) человека
        /// </summary>
        public string FulltName { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию фильмов,
        /// в которых этот человек был режиссером
        /// </summary>
        public ICollection<DbFilm> DbFilmsDirector { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию фильмов,
        /// в которых этот человек был актёром
        /// </summary>
        public ICollection<DbFilm> DbFilmsActor { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сериалов,
        /// в которых этот человек был режиссером
        /// </summary>
        public ICollection<DbTVSeries> DbTVSerialsDirector { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сериалов,
        /// в которых этот человек был актёром
        /// </summary>
        public ICollection<DbTVSeries> DbTVSerialsActor { get; set; }
    }
}
