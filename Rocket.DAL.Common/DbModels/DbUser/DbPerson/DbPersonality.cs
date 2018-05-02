namespace Rocket.DAL.Common.DbModels
{
    /// <summary>
    /// Представляет модель хранения данных о личности
    /// </summary>
    public class DbPersonality
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификационный номер сведений о личности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
    }
}
