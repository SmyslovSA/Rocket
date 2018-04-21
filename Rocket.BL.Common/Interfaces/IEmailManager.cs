namespace Rocket.BL.Common.Models
{
    /// <summary>
    /// Интерфейс для реализации механизма добавления нового или удаления одного из имеющихся e-mail
    /// </summary>
    interface IEmailManager
    {
        bool AddEmail(AuthorisedUser user, string email);
        bool DeleteEmail(AuthorisedUser user, string email);
    }
}
