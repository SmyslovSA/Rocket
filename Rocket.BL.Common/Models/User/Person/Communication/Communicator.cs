namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Идентификатор, как средство связи.
    /// Например:
    /// ICQ  - это 'Tool', перечисляемого типа
    /// </summary>
    public class Communicator
    {
        public int Id { get; set; }
        
        public string Idendificator { get; set; }

        public Tool Tool { get; set; }
    }
}