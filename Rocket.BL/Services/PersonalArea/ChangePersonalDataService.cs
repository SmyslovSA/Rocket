using AutoMapper;
using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.UoW;
using System.Linq;

namespace Rocket.BL.Services.PersonalArea
{
    public class ChangePersonalDataService : BaseService, IPersonalData
    {
        private readonly IValidator _validator;

        public ChangePersonalDataService(IUnitOfWork unitOfWork, IValidator<SimpleUser> validator) : base(unitOfWork)
        {
            _validator = validator;
        }

        /// <summary>
        /// Смена пароля.
        /// </summary>
        /// <param name="model">Принимает модель пользователя, инициировавшего смену пароля.</param>
        /// <param name="newPassword">Новый пароль.</param>
        /// <param name="newPasswordConfirm">Подтверждение пароля.</param>
        /// <returns>True - при смене пароля, false - при ошибках валидации.</returns>
        public bool ChangePasswordData(SimpleUser model, string newPassword, string newPasswordConfirm)
        {
            if (!PasswordValidate(newPassword, newPasswordConfirm))
            {
                return false;
            }

            var user = Mapper.Map<DbAuthorisedUser>(model);
            user.DbUser.Password = newPassword;
            _unitOfWork.UserAuthorisedRepository.Update(user);
            _unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// Смена персональных данных.
        /// </summary>
        /// <param name="model">Принимает модель пользователя, инициировавшего смену данных.</param>
        public void ChangePersonalData(SimpleUser model)
        {
            var validate = _validator.Validate(model);
            if (!validate.IsValid)
            {
                throw new ValidationException($"Error:{validate.Errors.Select(s => s.ErrorMessage)}");
            }

            var user = Mapper.Map<DbAuthorisedUser>(model);
            user.DbUser.FirstName = model.FirstName;
            user.DbUser.LastName = model.LastName;
            user.Avatar = model.Avatar;
            _unitOfWork.UserAuthorisedRepository.Update(user);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Приватный метод для проверки валидности пароля.
        /// </summary>
        /// <param name="password">Новый пароль.</param>
        /// <param name="passwordConfirm">Подтверждение пароля.</param>
        /// <returns>True - если пароль прошел валидацию, false - если не прошел.</returns>
        private bool PasswordValidate(string password, string passwordConfirm)
        {
            //TODO: написать нормальный метод валидации
            if (password == null || passwordConfirm == null)
            {
                return false;
            }

            return password == passwordConfirm && password.Length > 6;
        }
    }
}