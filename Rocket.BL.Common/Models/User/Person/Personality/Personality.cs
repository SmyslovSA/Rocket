namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Содержит идентификатор личности. Как правило,
    /// это константа в течение жизни (для мужчин, так точно),
    /// позволяющая однозначно идентифицировать личность
    /// </summary>
    public class Personality
    {
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
