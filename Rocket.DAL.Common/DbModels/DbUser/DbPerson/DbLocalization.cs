namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения данных о локализации человека пользователя
    /// </summary>
    public class DbLocalization
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификационный номер локализации
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задает или возвращает гражданство пользователя
        /// </summary>
        public Dbcountry Sitizenship { get; set; }

        /// <summary>
        /// Задает или возвращает язык пользователя
        /// </summary>
        public DbLanguage Language { get; set; }
    }
}
