namespace Rocket.BL.Common.Models
{
    /// <summary>
    /// Интерфейс для реализации механизма добавления нового или удаления одного из имеющихся e-mail
    /// </summary>
    interface IEmailManager
    {
        bool AddEmail(User user, string email);
        bool DeleteEmail(User user, string email);
    }
}
