namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Представляет информацию о человеке (режиссере, актёре)
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор человека
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает имя и фамилию (полное имя) человека
        /// </summary>
        public string FulltName { get; set; }
    }
}