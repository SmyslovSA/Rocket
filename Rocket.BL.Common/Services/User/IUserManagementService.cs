using Rocket.BL.Common.Models.User;
using System;
using System.Linq.Expressions;

namespace Rocket.BL.Common.Services.User
{
    /// <summary>
    /// Представляет сервис для работы с пользователем
    /// </summary>
    public interface IUserManagementService : IDisposable
    {
        /// <summary>
        /// Возвращает пользователя с заданным идентификатором
        /// из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Экземпляр пользователя</returns>
        Models.User.User GetUser(int id);

        /// <summary>
        /// Добавляет заданного пользователя в хранилище данных
        /// и возвращает идентификатор добавленного пользователя
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Уникальный идентификатор пользователя</returns>
        int AddUser(Models.User.User user);

        /// <summary>
        /// Обновляет информацию заданного пользователя в хранилище данных
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        void UpdateUser(Models.User.User user);

        /// <summary>
        /// Удаляет пользователя с заданным идентификатором
        /// из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        void DeleteUser(int id);

        /// <summary>
        /// После добавление пользователя в репозитарий 
        /// генерирует ссылку, по которой пользователь
        /// в случае получения уведомлении об активации, может 
        /// активировать аккаунт
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Ссылку для активации аккаунта</returns>
        string CreateConfirmationLink(Models.User.User user);

        /// <summary>
        /// Проверяет наличие пользователя в хранилище данных
        /// соответствующего заданному фильтру
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска пользователя</param>
        /// <returns>Возвращает <see langword="true"/>, если пользователь существует в хранилище данных</returns>
        bool UserExists(Expression<Func<Models.User.User, bool>> filter);
    }
}