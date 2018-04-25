namespace Rocket.DAL.Common.DbModels.Parser
{
    /// <summary>
    /// Сущность модели человека (режиссере, актёре или музыканте).
    /// </summary>
    public class PersonEntity
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Полное имя человека.
        /// </summary>
        public string FullName { get; set; }
    }
}
