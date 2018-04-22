namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Представляет информацию о человеке (режиссере, актёре или музыканте)
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор человека
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает имя человека
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Возвращает или задает фамилию человека
        /// </summary>
        public string LastName { get; set; }
    }
}