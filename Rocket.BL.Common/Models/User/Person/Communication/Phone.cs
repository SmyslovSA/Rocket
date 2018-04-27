namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Все понятно, думаю.
    /// </summary>
    public class Phone : IPhone
    {
        public string Number { get; set; }

        public uint? Code { get; set; }

        public PhoneType? Type { get; set; }

        public Accessability? Access { get; set; }
    }
}
