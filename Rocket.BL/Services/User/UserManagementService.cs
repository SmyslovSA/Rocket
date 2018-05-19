using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rocket.BL.Common.Services.User;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.User
{
    /// <summary>
    /// Представляет сервис для работы с пользователями
    /// в хранилище данных.
    /// </summary>
    public class UserManagementService : BaseService, IUserManagementService
    {   
        /// <summary>
        /// Создает новый экземпляр <see cref="UserManagementService"/>
        /// с заданным unit of work.
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work.</param>
        public UserManagementService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Возвращает всех пользователей
        /// из хранилища данных.
        /// </summary>
        /// <returns>Коллекция всех экземпляров пользователей.</returns>
        public ICollection<Common.Models.User.User> GetAllUsers()
        {
            var users = new List<Common.Models.User.User>();

            var usersCount = _unitOfWork.UserRepository.ItemsCount();

            if (usersCount == 0)
            {
                return null;
            }

            for (int i = 0; i < usersCount; i++)
            {
                users.Add(GetUser(i));
            }

            return users;
        }

        /// <summary>
        /// Возвращает пользователей
        /// из хранилища данных для пейджинга.
        /// </summary>
        /// <param name="pageSize">Количество сведений о пользователях, выводимых на страницу.</param>
        /// <param name="pageNumber">Номер выводимой страницы со сведениями о пользователях.</param>
        /// <returns>Коллекция экземпляров пользователей для пейджинга.</returns>
        public ICollection<Common.Models.User.User> GetUsersPage(int pageSize, int pageNumber)
        {
            var usersCount = _unitOfWork.UserRepository.ItemsCount();

            if (usersCount == 0)
            {
                return null;
            }

            var pagesCount = (int)Math.Ceiling((double)usersCount / pageSize);

            if (pageNumber > pagesCount)
            {
                return null;
            }

            //var pagesCount = (int)Math.Ceiling((double)usersCount / pageSize);

            //var startUserIndex = (pageNumber - 1)* pageSize;
            //var finishUserIndex = startUserIndex + pageNumber < pagesCount ? pageSize - 1 : usersCount /pageSize - 1;
            //for (var i = startUserIndex; i <= finishUserIndex; i++)
            //{
            //    users.Add(GetUser(i));
            //}

            //return users;

            var dbUsers = _unitOfWork.UserRepository.GetPage(pageSize, pageNumber);

            return dbUsers.Select(Mapper.Map<Common.Models.User.User>).ToList();
        }

        /// <summary>
        /// Возвращает пользователя с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Экземпляр пользователя.</returns>
        public Common.Models.User.User GetUser(int id)
        {
            return Mapper.Map<Common.Models.User.User>(
                _unitOfWork.UserRepository.GetById(id));
        }

        /// <summary>
        /// Добавляет заданного пользователя в хранилище данных
        /// и возвращает идентификатор добавленного пользователя.
        /// </summary>
        /// <param name="user">Экземпляр пользователя для добавления.</param>
        /// <returns>Идентификатор пользователя.</returns>
        public int AddUser(Common.Models.User.User user)
        {
            var dbUser = Mapper.Map<DbUser>(user);
            _unitOfWork.UserRepository.Insert(dbUser);
            _unitOfWork.SaveChanges();
            return dbUser.Id;
        }

        /// <summary>
        /// Обновляет информацию заданного пользователя в хранилище данных.
        /// </summary>
        /// <param name="user">Экземпляр пользователя для обновления.</param>
        public void UpdateUser(Common.Models.User.User user)
        {
            var dbUser = Mapper.Map<DbUser>(user);
            _unitOfWork.UserRepository.Update(dbUser);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Удаляет пользователя с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public void DeleteUser(int id)
        {
            _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Проверяет наличие пользователя в хранилище данных
        /// соответствующего заданному фильтру.
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска пользователя.</param>
        /// <returns>Возвращает <see langword="true"/>, если пользователь существует в хранилище данных.</returns>
        public bool UserExists(Expression<Func<Common.Models.User.User, bool>> filter)
        {
            return _unitOfWork.UserRepository.Get(
                           Mapper.Map<Expression<Func<DbUser, bool>>>(filter))
                      .FirstOrDefault() != null;
        }

        /// <summary>
        /// После добавление пользователя в репозитарий 
        /// генерирует ссылку, по которой пользователь
        /// в случае получения уведомлении об активации, может 
        /// активировать аккаунт.
        /// </summary>
        /// <param name="user">Экземпляр пользователя.</param>
        /// <returns>Ссылку для активации аккаунта.</returns>
        public string CreateConfirmationLink(Common.Models.User.User user)
        {
            return string.Empty;
        }
    }
}