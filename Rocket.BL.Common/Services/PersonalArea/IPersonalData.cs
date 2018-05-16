using Rocket.BL.Common.Models.PersonalArea;

namespace Rocket.BL.Common.Services.PersonalArea
{
    /// <summary>
    /// Интерфейс для работы с личными данными AuthorisedUser.
    /// </summary>
    public interface IPersonalData
    {
        /// <summary>
        /// Изменение личных данных (ФИО, аватар).
        /// </summary>
        /// <param name="user">Модель пользователя, инициировавшего смену личных данных.</param>
        void ChangePersonalData(SimpleUser user);

        /// <summary>
        /// Изменение пароля на новый.
        /// </summary>
        /// <param name="user">Модель пользователя, инициировавшего смену пароля.</param>
        /// <param name="newPassword">Новый пароль, введенный пользователем.</param>
        /// <param name="newPasswordConfirm">Подтверждение пароля.</param>
        /// <returns>
        /// True - при успешном изменении пароля.
        /// </returns>
        bool ChangePasswordData(SimpleUser user, string newPassword, string newPasswordConfirm);
    }
}