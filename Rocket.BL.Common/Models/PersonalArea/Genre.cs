namespace Rocket.BL.Common.Models.PersonalArea
{
    /// <summary>
    /// класс, содержащий данные о жанрах фильма, сериала или музыки
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Id жанра
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя жанра
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// категория к которой относится данный жанр
        /// </summary>
        public Category Category { get; set; }
    }
}