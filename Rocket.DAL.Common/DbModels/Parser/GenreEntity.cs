namespace Rocket.DAL.Common.DbModels.Parser
{
    /// <summary>
    /// Сущность модели жанра.
    /// </summary>
    public class GenreEntity
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        public int CategoryCode { get; set; }

        public CategoryEntity Category { get; set; }
    }
}
