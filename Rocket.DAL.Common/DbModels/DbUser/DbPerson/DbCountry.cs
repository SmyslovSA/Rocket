namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения данных о стране для пользователя
    /// </summary>
    public class Dbcountry
    {
        /// <summary>
        /// Задает или возвращает уникальный идентификатор языка пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задает или возвращает название языка пользователя
        /// </summary>
        public string Name { get; set; }
    }
}
