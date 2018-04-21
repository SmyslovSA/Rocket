namespace Rocket.BL.Common.Models
{
    /// <summary>
    /// интерфейс для изменения личных данных (ФИО, аватар), а также для изменения пароля пользователя
    /// </summary>
    interface IPersonalData
    {
        bool ChangePersonalData(AuthorisedUser user, string firstName, string lastName, string avatar);
        bool ChangePasswordData(AuthorisedUser user, string newPassword, string newPasswordConfirm);
    }
}
