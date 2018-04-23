using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель персонального списка релизов
    /// </summary>
    public class DbPersonalizedTape
    {
        /// <summary>
        /// ID списка
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// список релизов в виде "категория - список жанров"
        /// </summary>
        public IDictionary<DbCategory,ICollection<DbGenre>> Tape { get; set; }
    }
}