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
        /// внешний  ключ к таблице Category
        /// </summary>
        public int? CategoryId { get; set; }
        /// <summary>
        /// ссылка на связанную Category
        /// </summary>
        public DbCategory Category { get; set; }
        /// <summary>
        /// коллекция User подписанных на данный жанр
        /// </summary>
        public ICollection<DbUser> Users { get; set; }
    }
}