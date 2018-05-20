namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Представляет информацию о жанре фильма или сериала
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор жанра
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает название жанра
        /// </summary>
        public string Name { get; set; }
    }
}