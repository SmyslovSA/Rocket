namespace Rocket.BL.Common.Models.User.Person.Localization
{
    /// <summary>
    /// Локализация пользователя
    /// </summary>
    public class Localization
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификационный номер локализации
        /// </summary>
        public int Id { get; set; }
        
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
