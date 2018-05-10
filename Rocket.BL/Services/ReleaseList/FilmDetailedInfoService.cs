using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Services.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.UoW;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.BL.Services.ReleaseList
{
    /// <summary>
    /// Представляет сервис для работы с детальной информацией
    /// о фильмах в хранилище данных
    /// </summary>
    public class UserDetailedInfoService : BaseService, IUserDetailedInfoService
    {
        /// <summary>
        /// Создает новый экземпляр <see cref="UserDetailedInfoService"/>
        /// с заданным unit of work
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        public UserDetailedInfoService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Возвращает фильма с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns>Экземпляр фильма</returns>
        public User GetUser(int id)
        {
            return Mapper.Map<User>(
                this._unitOfWork.UserRepository.GetById(id));
        }

        /// <summary>
        /// Добавляет заданный фильм в хранилище данных
        /// и возвращает идентификатор добавленного фильма.
        /// </summary>
        /// <param name="film">Экземпляр фильма для добавления</param>
        /// <returns>Идентификатор фильма</returns>
        public int AddUser(User film)
        {
            var dbUser = Mapper.Map<DbUser>(film);
            this._unitOfWork.UserRepository.Insert(dbUser);
            this._unitOfWork.Save();
            return dbUser.Id;
        }

        /// <summary>
        /// Обновляет информацию заданного фильма в хранилище данных
        /// </summary>
        /// <param name="film">Экземпляр фильма для обновления</param>
        public void UpdateUser(User film)
        {
            var dbUser = Mapper.Map<DbUser>(film);
            this._unitOfWork.UserRepository.Update(dbUser);
            this._unitOfWork.Save();
        }

        /// <summary>
        /// Удаляет фильм с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        public void DeleteUser(int id)
        {
            this._unitOfWork.UserRepository.Delete(id);
            this._unitOfWork.Save();
        }

        /// <summary>
        /// Проверяет наличие фильма в хранилище данных
        /// соответствующего заданному фильтру
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска фильма</param>
        /// <returns>Возвращает <see langword="true"/>, если фильм существует в хранилище данных</returns>
        public bool UserExists(Expression<Func<User, bool>> filter)
        {
            return this._unitOfWork.UserRepository.Get(
                Mapper.Map<Expression<Func<DbUser, bool>>>(filter))
                .FirstOrDefault() != null;
        }
    }
}
