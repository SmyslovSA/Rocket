namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Тип пароля. Докомпозирован
    /// требованиям к содержимому пароля.
    /// </summary>
    public class Password : IPassword
    {
        public string Content { get; set; } // Содержимое пароля.

        public Rule Template { get; set; } // Шаблон пароля
    }
}
