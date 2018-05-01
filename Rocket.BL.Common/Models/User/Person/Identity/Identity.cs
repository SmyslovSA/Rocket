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
        DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Задает или возвращает пол пользователя
        /// </summary>
        Gender? Gender { get; set; }
    }
}
