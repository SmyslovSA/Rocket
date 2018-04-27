namespace Rocket.BL.Common.Models
{
    interface IPersonalData
    {
        /// <summary>
        /// изменение личных данных (ФИО, аватар)
        /// </summary>
        /// <param name="userId">id пользователя, инициировавшего смену личных данных</param>
        /// <param name="firstName">новое имя пользователя</param>
        /// <param name="lastName">новая фамилия пользователя</param>
        /// <param name="avatar">новый относительный путь к аватару пользователя</param>
        /// <returns>
        /// true - при успешном изменении данных пользователя
        /// </returns>
        bool ChangePersonalData(int userId, string firstName, string lastName, string avatar);
        /// <summary>
        /// изменение пароля на новый
        /// </summary>
        /// <param name="userId">id пользователя, инициировавшего смену пароля</param>
        /// <param name="newPassword">новый пароль, введенный пользователем</param>
        /// <param name="newPasswordConfirm">подтверждение пароля</param>
        /// <returns>
        /// true - при успешном изменении пароля
        /// </returns>
        bool ChangePasswordData(int userId, string newPassword, string newPasswordConfirm);
    }
}
