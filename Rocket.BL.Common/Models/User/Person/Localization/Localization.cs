using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Локализация пользователя
    /// </summary>
    public class Localization
    {
        /// <summary>
        /// Задает или возвращает гражданство пользователя
        /// </summary>
        public Country Sitizenship { get; set; }
            
        /// <summary>
        /// Задает или возвращает язык пользователя
        /// </summary>
        public Language Language { get; set; }
    }
}
