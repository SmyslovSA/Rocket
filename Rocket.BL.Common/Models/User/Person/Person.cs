namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Все, что относится к личности человека.
    /// Что, более менее неизменно в рамках небольших
    /// отрезков жизни, характеризует, как личность.
    /// </summary>
    public class Person : IPerson 
    {
        public int Id { get; set; }
            
        // Идентификатор. Имя, отчество, фамилия.
        public Identity Identity { get; set; }

        // Каналы связи.
        public Communication Communication { get; set; }

        // Национальность, включая язык.
        public Nationality Nationality { get; set; }

        // Тут все классически - адрес.
        public IAddress Address { get; set; }

        // Дата рождения и пол.
        public IPersonality Personality { get; set; }
    }
}
