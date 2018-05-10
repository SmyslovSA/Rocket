using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.ReleaseList
{
    /// <summary>
    /// Представляет модель хранения данных о странах
    /// </summary>
    public class DbCountry
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор страны
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает название страны
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию фильмов,
        /// в съёмках которых принимала участие эта страна.
        /// </summary>
        public ICollection<DbUser> DbUsers { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сериалов,
        /// в съёмках которых принимала участие эта страна
        /// </summary>
        public ICollection<DbTVSeries> DbTVSerials { get; set; }
    }
}