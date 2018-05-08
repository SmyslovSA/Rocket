using Rocket.BL.Common.Models.PersonalArea;

namespace Rocket.BL.Common.Services.PersonalArea
{
    /// <summary>
    /// интерфейс для работы с личными данными User
    /// </summary>
    public interface IPersonalData
    {
        /// <summary>
        /// изменение личных данных (ФИО, аватар)
        /// </summary>
        /// <param name="user">модель пользователя, инициировавшего смену личных данных</param>
        void ChangePersonalData(SimpleUser user);
        /// <summary>
        /// изменение пароля на новый
        /// </summary>
        /// <param name="user">модель пользователя, инициировавшего смену пароля</param>
        /// <param name="newPassword">новый пароль, введенный пользователем</param>
        /// <param name="newPasswordConfirm">подтверждение пароля</param>
        /// <returns>
        /// true - при успешном изменении пароля
        /// </returns>
        bool ChangePasswordData(SimpleUser user, string newPassword, string newPasswordConfirm);
    }
}
