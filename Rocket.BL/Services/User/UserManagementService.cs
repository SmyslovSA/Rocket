using AutoMapper;
using Rocket.DAL.Common.Repositories.User;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.User
{
    /// <summary>
    /// Представляет сервис для работы с пользователями
    /// в хранилище данных
    /// </summary>
    public class UserManagementService : BaseService, Common.Services.User.IUserManagementService
    {   
        private readonly IDbUserRepository _userRepository;

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
               this._unitOfWork.FilmRepository.GetById(id));
        }

        /// <summary>
        /// Добавляет заданного пользователя в хранилище данных
        /// и возвращает идентификатор добавленного пользователя.
        /// </summary>
        /// <param name="user">Экземпляр пользователя для добавления</param>
        /// <returns>Идентификатор пользователя</returns>
        public int AddUser(Common.Models.User.User user)
        {
            if (user == null)
            {
                return -1;
            }
            
            // Экземпляр сервиса для валидации экземпляра пользователя
            var userValidateService = new UserValidateService(new );

            // Валидация экземпляра пользователя
            if (!userValidateService.UserValidateOnAddition(user))
            {
                return -1;
            }

            // Проверка дубля
            if (this.UserExists(user))
            {
                return -1;
            }

            // Добавление пользователя в репозитарий
            var dbUser = Mapper.Map<DbUser>(user);
            this._unitOfWork.UserRepository.Insert(dbUser);
            this._unitOfWork.Save();

            // Получение добавленного пользователя, чтобы скопировать его Id
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
        /// соответствующего заданному экземпляру
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Возвращает <see langword="true"/>, если пользователь существует в хранилище данных</returns>
        public bool UserExists(Common.Models.User.User user)
        {
            return false;
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
