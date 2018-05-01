namespace Rocket.BL.Common.Models.User
{
    using System;

    /// <summary>
    /// Представляет сведения, неотъемлемые от личности
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Задает или возвращает дату рождения пользователя
        /// </summary>
        public  DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Задает или возвращает пол пользователя
        /// </summary>
        public Gender? Gender { get; set; }
    }
}
