using Rocket.BL.Common.Models.User;

namespace Rocket.BL.Common.Services.User
{
    /// <summary>
    /// Представляет сервис для работы со статусом аккаунта
    /// аккаунта
    /// </summary>
    public interface IUserAccountStatusService
    {
        /// <summary>
        /// Получает статус аккаунта пользователя с определенным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Статус аккаунта пользователя</returns>
        AccountStatus GetUserAccountStatus(int id);

        /// <summary>
        /// Получает статус аккаунта пользователя
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Статус аккаунта пользователя</returns>
        AccountStatus GetUserAccountStatus(Models.User.User user);

        /// <summary>
        /// Задает статус аккаунта пользователя с определенным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="statusType">Статус аккаунта пользователя</param>
        void SetUserAccoutStatus(int id, AccountStatus AccountStatus);

        /// <summary>
        /// Задает статус аккаунта пользователя
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <param name="statusType">Статус аккаунта пользователя</param>
        void SetUserAccountStatus(Models.User.User user, AccountStatus AccountStatus);
    }
}
