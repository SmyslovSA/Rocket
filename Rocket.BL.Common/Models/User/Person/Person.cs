namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Все, что относится к личности человека.
    /// Что, более менее неизменно в рамках небольших
    /// отрезков жизни, характеризует, как личность.
    /// </summary>
    public class Person
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя и фамилия
        /// </summary>
        public Identity Identity { get; set; }

        /// <summary>
        /// Контактная информация (телефоны, Email, др.)
        /// </summary>
        public Communication Communication { get; set; }

        /// <summary>
        /// Возвращает или задает язык и национальность
        /// </summary>
        public Nationality Nationality { get; set; }

        /// <summary>
        /// Возвращает или задает дату рождения и пол
        /// </summary>
        public Personality Personality { get; set; }
    }
}
