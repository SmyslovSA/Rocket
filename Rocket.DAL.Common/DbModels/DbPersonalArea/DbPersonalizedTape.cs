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
        /// внешний ключ к таболице User
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// ссылка на Usera
        /// </summary>
        public DbUser User { get; set; }
        /// <summary>
        /// внешний ключ к таблице Category
        /// </summary>
        public int? CategoryId { get; set; }
        /// <summary>
        /// ссылка на Category
        /// </summary>
        public DbCategory Category { get; set; }
        /// <summary>
        /// внешний ключ к таблице Genre
        /// </summary>
        public int? GenreId { get; set; }
        /// <summary>
        /// ссылка на Genre
        /// </summary>
        public DbGenre Genre { get; set; }
    }
}