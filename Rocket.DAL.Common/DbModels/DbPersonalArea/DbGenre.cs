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
    }
}