namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Все, что относится к личности человека.
    /// Что, более менее неизменно в рамках небольших
    /// отрезков жизни, характеризует, как личность.
    /// </summary>
    public interface IPerson
    {
       int Id { get; }
        
        // Идентификатор. Имя, отчество, фамилия.
        Identity Identity { get; set; }
        
        // Каналы связи.
        Communication Communication { get; set; }

        // Национальность, включая язык.
        Nationality Nationality { get; set; }

        // Тут все классически - адрес.
        IAddress Address { get; set; }

        // Дата рождения и пол.
        IPersonality Personality { get; set; }
    }
}
