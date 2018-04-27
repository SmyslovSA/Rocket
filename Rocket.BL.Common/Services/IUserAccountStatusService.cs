using Rocket.BL.Common.Models.User;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Устанавливает и получает значение статуса 
    /// аккаунта, значения статуса аккаунта заданы
    /// перечислением 'StatusType', принимающее значение
    /// Registered = 1, Activated = 2, и так далее...
    /// </summary>
    public interface IUserAccountStatusService
    {
        /// <summary>
        /// Получает статус аккаунта пользователя с
        /// определенным идентификатором.
        /// Реализуем принцип чтение-записи параметра.
        /// Account сложное свойство типа User
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Статус аккаунта пользователя</returns>
        StatusType GetUserAccountStatus(int id);

        /// <summary>
        /// Перегрузка метода по получению статуса аккаунта,
        /// где вместо идентификатора пользователя используется 
        /// экземпляр пользователя.
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Статус аккаунта пользователя</returns>
        StatusType GetUserAccountStatus(IUser user);

        /// <summary>
        /// Задаем значение статуса экземпляра пользователя
        /// на основании идентификатора.
        /// Реализуем принцип чтение-записи параметра.
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="statusType">Статус аккаунта пользователя</param>
        void SetUserAccoutStatus(int id, StatusType statusType);

        /// <summary>
        /// Перегрузка метода. Задаем статус пользователя на основании 
        /// его экземпляра.
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <param name="statusType">Статус аккаунта пользователя</param>
        void SetUserAccountStatus(IUser user, StatusType statusType);
    }
}
