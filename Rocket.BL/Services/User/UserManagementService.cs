using System;
using AutoMapper;
using Rocket.DAL.Common.Repositories.IDbUserRepository;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.BL.Services.User
{
    public class UserManagementService : Common.Services.User.IUserManagementService
    {   
        private readonly IDbUserRepository _userRepository;

        public UserManagementService(IDbUserRepository userRepository)
        {
            this._userRepository = userRepository;
            Func<DbUser, string, bool> _filter = (u, s) => u.Login == s;
        }
        public Common.Models.User.User GetUser(int Id)
        {
            return null;
        }
        
        public int AddUser(Common.Models.User.User user)
        {
            if (user == null)
            {
                return -1;
            }
            
            // Экземпляр сервиса для валидации экземпляра пользователя
            var userValidateService = new UserValidateService();

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
            this._userRepository.AddUserToStore(dbUser);

            // Получение добавленного пользователя, чтобы скопировать его Id
            dbUser = this._userRepository.GetByUserLoginFromStore(dbUser.Login);

            return dbUser.Id;
        }

        public void UpdateUser(Common.Models.User.User user)
        {
        }

        public void DeleteUser(int Id)
        {
        }

        public bool UserExists(Common.Models.User.User user)
        {
            return false;
        }

        public string CreateConfirmationLink(Common.Models.User.User user)
        {
            return string.Empty;
        }
    }
}
