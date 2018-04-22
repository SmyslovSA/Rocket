using System;

namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Представляет информацию о выходе произведения (фильма, серии, музыкального альбома)
    /// </summary>
    public class Release
    {
        /// <summary>
        /// Возвращает или задает дату выхода произведения
        /// </summary>
        public DateTime ReleaseDate { get; set; }
    }
}