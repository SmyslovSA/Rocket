using System;
using Rocket.BL.Common.Models.User;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Сервис управления пользователем. Набор CRUD 
    /// операций. Рестрация пользователя (Create), получение или чтение (Read) по 
    /// идентификатору, обновление (Update) и удаление (Delete), соответственно.
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Добавляет пользователям в хранилище данных
        /// и возвращает идентификатор добавленного пользователя.
        /// То есть, фактически, операция регистрации.
        /// Create операция в CRUD.
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns></returns>
        int AddUser(User user);

        /// <summary>
        /// Возвращает пользователя с заданным идентификатором
        /// из хранилища данных.
        /// Read операция CRUD.
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        User GetUser(int id);

        /// <summary>
        /// Обновляет информацию заданного пользователя в хранилище данных.
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        void UpdateUser(User user);

        /// <summary>
        /// Удаляет пользователя с заданным идентификатором
        /// из хранилища данных.
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
