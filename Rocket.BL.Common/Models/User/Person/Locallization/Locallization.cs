using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Локализация пользователя
    /// </summary>
    public class Locallization
    {
        /// <summary>
        /// Задает или возвращает гражданство пользователя
        /// </summary>
        Country Sitizenship { get; set; }
        
        /// <summary>
        /// Задает или возвращает язык пользователя
        /// </summary>
        Language Language { get; set; }
    }
}
