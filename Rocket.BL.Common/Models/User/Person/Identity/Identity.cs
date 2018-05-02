using System;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Представляет сведения, неотъемлемые от личности
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификационный номер сведений о личности
        /// </summary>
        public int Id { get; set; }
        
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
