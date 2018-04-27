namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Содержит идентификатор личности. Как правило,
    /// это константа в течение жизни (для мужчин, так точно),
    /// позволяющая однозначно идентифицировать личность.
    /// Ради смеха, но пол, уже часто не является такой идентификацией.
    /// </summary>
    public class Identity
    {
        // Имя.
        public string FirstName { get; set; }

        // Фамилия.
        public string SecondName { get; set; }

        // Отчество.
        public string Family { get; set; }
    }
}
