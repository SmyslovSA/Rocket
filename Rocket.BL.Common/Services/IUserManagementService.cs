using Rocket.BL.Common.Models.User;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Представляет сервис для работы с пользователем
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Возвращает пользователя с заданным идентификатором
        /// из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Экземпляр пользователя</returns>
        User GetUser(int id);

        /// <summary>
        /// Добавляет заданного пользователя в хранилище данных
        /// и возвращает идентификатор добавленного пользователя
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Уникальный идентификатор пользователя</returns>
        int AddUser(User user);

        /// <summary>
        /// Обновляет информацию заданного пользователя в хранилище данных
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        void UpdateUser(User user);

        /// <summary>
        /// Удаляет пользователя с заданным идентификатором
        /// из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        void DeleteUser(int id);

        /// <summary>
        /// Проверяет наличие заданного пользователя в 
        /// хранилище данных
        /// </summary>
        /// <param name="user">Экземпляр пользователя для проверки</param>
        /// <returns>Возвращает <see langword='true'/>, если пользователь существует в хранилище данных</returns>
        bool UserExists(User user);
    }
}
