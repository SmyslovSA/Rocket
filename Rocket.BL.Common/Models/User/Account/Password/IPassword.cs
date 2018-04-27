namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Интерфейс пароля. Докомпозирован
    /// требованиям к содержимому пароля - тип 'Rule'.
    /// </summary>
    public interface IPassword
    {
        string Content { get; set; } // Содержимое пароля.

        Rule Template { get; set; } // Шаблон пароля
    }
}
