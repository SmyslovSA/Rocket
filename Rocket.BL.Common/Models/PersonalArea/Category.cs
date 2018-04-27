namespace Rocket.BL.Common.Models
{
    /// <summary>
    /// класс, содержащий данные о категории (фильм, сериал или музыка)
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id категории
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя категории
        /// </summary>
        public string Name { get; set; }
    }
}