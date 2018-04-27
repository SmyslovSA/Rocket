namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Все понятно, думаю.
    /// </summary>
    public interface IPhone
    {
        string Number { get; set; }

        uint? Code { get; set; }

        // Тип номера.
        PhoneType? Type { get; set; }
        
        // Доступ к номеру. Возможен,
        // но не разрешен и так далее.
        Accessability? Access { get; set; }
    }
}
