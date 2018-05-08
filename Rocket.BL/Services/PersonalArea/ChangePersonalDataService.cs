using AutoMapper;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.UoW;
using FluentValidation;
using System.Linq;

namespace Rocket.BL.Services.PersonalArea
{
    public class ChangePersonalDataService : BaseService, IPersonalData
    {
        private readonly IValidator _validator;

        public ChangePersonalDataService(IUnitOfWork unitOfWork,IValidator<SimpleUser> validator) : base(unitOfWork)
        {
            _validator = validator;
        }

        /// <summary>
        /// смена пароля
        /// </summary>
        /// <param name="model">принимает модель пользователя, инициировавшего смену пароля</param>
        /// <param name="newPassword">новый пароль</param>
        /// <param name="newPasswordConfirm">подтверждение пароля</param>
        /// <returns>true - при смене пароля, false - при ошибках валидации</returns>
        public bool ChangePasswordData(SimpleUser model, string newPassword, string newPasswordConfirm)
        {
            if (!PasswordValidate(newPassword, newPasswordConfirm))
            {
                return false;
            }
            var user = Mapper.Map<DbAuthorisedUser>(model);
            user.DbAccount.Password = newPassword;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// смена персональных данных
        /// </summary>
        /// <param name="model">принимает модель пользователя, инициировавшего смену данных</param>
        public void ChangePersonalData(SimpleUser model)
        {
            var validate = _validator.Validate(model);
            if (!validate.IsValid)
            {
                throw new ValidationException($"Error:{ validate.Errors.Select(s => s.ErrorMessage)}");
            }
            var user = Mapper.Map<DbAuthorisedUser>(model);
            user.DbPersonality.FirstName = model.FirstName;
            user.DbPersonality.LastName = model.LastName;
            user.Avatar = model.Avatar;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
        }

        /// <summary>
        /// приватный метод для проверки валидности пароля
        /// </summary>
        /// <param name="password">пароль</param>
        /// <param name="passwordConfirm">подтверждение пароля</param>
        /// <returns>true - если пароль прошел валидацию, false - если не прошел</returns>
        private bool PasswordValidate(string password, string passwordConfirm)
        {
            //TODO: нааписать нормальный метод валидации
            if (password == null || passwordConfirm == null)
                return false;
            return password == passwordConfirm && password.Length > 6;
        }
    }
}
