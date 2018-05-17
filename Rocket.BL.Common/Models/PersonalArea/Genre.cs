namespace Rocket.BL.Common.Models.PersonalArea
{
    /// <summary>
    /// Класс, содержащий данные о жанрах фильма, сериала или музыки.
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Id жанра определенной категории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя жанра определенной категории.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Категория к которой относится данный жанр.
        /// </summary>
        public Category Category { get; set; }
    }
}