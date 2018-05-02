namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Представляет информацию о стране для пользователя
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Задает или возвращает уникальный идентификатор страны
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задает или возвращает название страны
        /// </summary>
        public string Name { get; set; }
    }
}
