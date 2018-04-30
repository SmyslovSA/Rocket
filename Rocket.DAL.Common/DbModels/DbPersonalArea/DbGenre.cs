using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель хранения данных жанров фильмов, сериалов и музыки
    /// </summary>
    public class DbGenre
    {
        /// <summary>
        /// Id жанра
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// название жанра
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// связь многие-ко-многим с таблицей DbCategory
        /// </summary>
        public virtual ICollection<DbCategory> Categories { get; set; }

        public virtual ICollection<DbPersonalizedTape> PersonalizedTape { get; set; }
    }
}