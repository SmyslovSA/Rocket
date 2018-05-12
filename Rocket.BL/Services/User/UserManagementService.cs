using AutoMapper;
using Rocket.BL.Common.Services.User;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Common.UoW;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.BL.Services.User
{
    /// <summary>
    /// Представляет сервис для работы с пользователями
    /// в хранилище данных
    /// </summary>
    public class UserManagementService : BaseService, IUserManagementService
    {   
        /// <summary>
        /// Создает новый экземпляр <see cref="UserManagementService"/>
        /// с заданным unit of work
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        public UserManagementService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Возвращает пользователя с заданным идентификатором из хранилища данных 
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Экземпляр пользователя</returns>
        public Common.Models.User.User GetUser(int id)
        {
            return Mapper.Map<Common.Models.User.User>(
               this._unitOfWork.UserRepository.GetById(id));
        }

        /// <summary>
        /// Добавляет заданного пользователя в хранилище данных
        /// и возвращает идентификатор добавленного пользователя.
        /// </summary>
        /// <param name="user">Экземпляр пользователя для добавления</param>
        /// <returns>Идентификатор пользователя</returns>
        public int AddUser(Common.Models.User.User user)
        {
            var dbUser = Mapper.Map<DbUser>(user);
            this._unitOfWork.UserRepository.Insert(dbUser);
            this._unitOfWork.Save();
            return dbUser.Id;
        }

        /// <summary>
        /// Обновляет информацию заданного пользователя в хранилище данных
        /// </summary>
        /// <param name="user">Экземпляр пользователя для обновления</param>
        public void UpdateUser(Common.Models.User.User user)
        {
            var dbUser = Mapper.Map<DbUser>(user);
            this._unitOfWork.UserRepository.Update(dbUser);
            this._unitOfWork.Save();
        }

        /// <summary>
        /// Удаляет пользователя с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public void DeleteUser(int id)
        {
            this._unitOfWork.UserRepository.Delete(id);
            this._unitOfWork.Save();
        }

        /// <summary>
        /// Проверяет наличие пользователя в хранилище данных
        /// соответствующего заданному фильтру
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска пользователя</param>
        /// <returns>Возвращает <see langword="true"/>, если пользователь существует в хранилище данных</returns>
        public bool UserExists(Expression<Func<Common.Models.User.User, bool>> filter)
        {
            return this._unitOfWork.UserRepository.Get(
                Mapper.Map<Expression<Func<DbUser, bool>>>(filter))
                .FirstOrDefault() != null;
        }

        /// <summary>
        /// После добавление пользователя в репозитарий 
        /// генерирует ссылку, по которой пользователь
        /// в случае получения уведомлении об активации, может 
        /// активировать аккаунт
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Ссылку для активации аккаунта</returns>
        public string CreateConfirmationLink(Common.Models.User.User user)
        {
            return string.Empty;
        }
    }
}
