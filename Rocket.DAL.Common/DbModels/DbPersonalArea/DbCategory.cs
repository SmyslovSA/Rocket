using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель хранения данных категорий фильмов, сериалов и музыки
    /// </summary>
    public class DbCategory
    {
        /// <summary>
        /// Id категории
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// название категории
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// связь многие-ко-многим с таблицей DbGenre
        /// </summary>
        public ICollection<DbGenre> Genres { get; set; }
    }
}