namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Все, что относится к личности человека.
    /// То, что более менее неизменно в рамках небольших
    /// отрезков жизни и характеризует, как личность.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Задает или возвращает уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает имя и фамилию пользователя
        /// </summary>
        public Personality Personality { get; set; }

        /// <summary>
        /// Возвращает или задает контактную информацию пользователя (телефон, Email, адрес и так далее)
        /// </summary>
        public Communication Communication { get; set; }

        /// <summary>
        /// Возвращает или задает язык и гражданство пользователя
        /// </summary>
        public Localization Locallization { get; set; }

        /// <summary>
        /// Возвращает или задает дату рождения и пол
        /// </summary>
        public Identity Identity { get; set; }
    }
}
