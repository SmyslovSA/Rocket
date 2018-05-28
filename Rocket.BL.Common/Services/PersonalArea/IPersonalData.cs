using Rocket.BL.Common.Models.PersonalArea;

namespace Rocket.BL.Common.Services.PersonalArea
{
    /// <summary>
    /// Интерфейс для работы с личными данными AuthorisedUser.
    /// </summary>
    public interface IPersonalData
    {
        /// <summary>
        /// Получение модели авторизованного пользователя по Id
        /// </summary>
        /// <param name="id">Id по которому необходимо найти пользователя</param>
        /// <returns></returns>
        SimpleUser GetUserData(int id);

        /// <summary>
        /// Изменение личных данных (ФИО, аватар).
        /// <param name="id">Id пользователя, инициировавшего смену личных данных.</param>
        /// <param name="firstName">Имя пользователя</param>
        /// <param name="lastName">Фамилия пользователя</param>
        /// <param name="avatar">Аватар пользователя</param>
        void ChangePersonalData(int id, string firstName, string lastName, string avatar);

        /// <summary>
        /// Изменение пароля на новый.
        /// </summary>
        /// <param name="id">Id пользователя, инициировавшего смену пароля.</param>
        /// <param name="newPassword">Новый пароль, введенный пользователем.</param>
        /// <param name="newPasswordConfirm">Подтверждение пароля.</param>
        /// <returns>
        /// True - при успешном изменении пароля.
        /// </returns>
        void ChangePasswordData(int id, string newPassword, string newPasswordConfirm);
    }
}