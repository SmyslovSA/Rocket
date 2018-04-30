﻿namespace Rocket.BL.Common.Models
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
    }
}