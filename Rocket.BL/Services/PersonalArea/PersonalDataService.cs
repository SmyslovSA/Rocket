using AutoMapper;
using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.BL.Properties;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Common.UoW;
using System.Linq;

namespace Rocket.BL.Services.PersonalArea
{
    public class PersonalDataService : BaseService, IPersonalData
    {
        private readonly IValidator _validator;

        public PersonalDataService(IUnitOfWork unitOfWork, IValidator<Common.Models.User.User> validator) : base(unitOfWork)
        {
            _validator = validator;
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        public SimpleUser GetUserData(int id)
        {
            return Mapper.Map<SimpleUser>(_unitOfWork.UserAuthorisedRepository.Get(
                    f => f.DbUserId == id,
                    includeProperties: $"{nameof(DbUser)}")
                    ?.FirstOrDefault());
        }
        /// <summary>
        /// Смена пароля.
        /// </summary>
        /// <param name="id">Id пользователя, инициировавшего смену пароля.</param>
        /// <param name="newPassword">Новый пароль.</param>
        /// <param name="newPasswordConfirm">Подтверждение пароля.</param>
        /// <returns>True - при смене пароля, false - при ошибках валидации.</returns>
        public void ChangePasswordData(int id, string newPassword, string newPasswordConfirm)
        {
            if (!PasswordValidate(newPassword, newPasswordConfirm))
            {
                throw new ValidationException(Resources.UserWrongPassword);
            }

            var user = _unitOfWork.UserRepository.GetById(id);
            user.Password = newPassword;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Смена персональных данных.
        /// </summary>
        /// <param name="id">Id пользователя, инициировавшего смену личных данных.</param>
        /// <param name="firstName">Имя пользователя</param>
        /// <param name="lastName">Фамилия пользователя</param>
        /// <param name="avatar">Аватар пользователя</param>
        public void ChangePersonalData(int id, string firstName, string lastName, string avatar)
        {
            var user = _unitOfWork.UserRepository.Get(
                        f => f.Id == id,
                        includeProperties: $"{nameof(DbAuthorisedUser)}")
                        ?.FirstOrDefault();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.DbAuthorisedUser.Avatar = avatar;
            var userToValidate = Mapper.Map<Common.Models.User.User>(user);
            var validate = _validator.Validate(userToValidate);
            if (!validate.IsValid)
            {
                throw new ValidationException(Resources.UserWrongFirstOrLastName);
            }

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Приватный метод для проверки валидности пароля.
        /// </summary>
        /// <param name="password">Новый пароль.</param>
        /// <param name="passwordConfirm">Подтверждение пароля.</param>
        /// <returns>True - если пароль прошел валидацию, false - если не прошел.</returns>
        private bool PasswordValidate(string password, string passwordConfirm)
        {
            if (password == null || passwordConfirm == null)
            {
                return false;
            }

            return password == passwordConfirm && password.Length > 6;
        }
    }
}