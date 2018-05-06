using FluentValidation;
using System;
using Rocket.BL.Properties;

namespace Rocket.BL.Services.User
{
    public class UserValidateService : Rocket.BL.Common.Services.User.IUserValidateService
    {
        public bool UserValidateOnAddition(Common.Models.User.User user)
        {
            if (!UserValidateOnAdditionCheckRequiredFields(user))
            {
                return false;
            }

            if (!UserValidateOnAdditionCheckLogicAndFormat(user))
            {
                return false;
            }

            return true;
        }

        public bool UserValidateOnUpdating(Common.Models.User.User user)
        {
            return true;
        }

        /// <summary>
        /// Проверяем наличие обязательных полей в модели
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Возвращает истинно, если проверка прошла успешно</returns>
        public bool UserValidateOnAdditionCheckRequiredFields(Common.Models.User.User user)
        {
            UserValidatorCheckRequiredFields validator = new UserValidatorCheckRequiredFields();

            return validator.Validate(user).IsValid ? true : false;
        }

        /// <summary>
        /// Проверяем формат и другие условия валидации данных модели пользователя
        /// </summary>
        /// <param name="user">Экземпляр пользователя</param>
        /// <returns>Возвращает истинно, если проверка прошла успешно</returns>
        public bool UserValidateOnAdditionCheckLogicAndFormat(Common.Models.User.User user)
        {
            UserValidatorLogicAndFormat validator = new UserValidatorLogicAndFormat();

            return validator.Validate(user).IsValid ? true : false;
        }

        /// <summary>
        /// Задаем условия для валидатора данных аккаунта
        /// </summary>
        internal class UserValidatorLogicAndFormat : AbstractValidator<Common.Models.User.User>
        {
            internal UserValidatorLogicAndFormat()
            {
                /// <summary>
                /// Содержит строку с сообщением и минимальным количеством символов,
                /// которые должны быть в пароле
                /// </summary>
                string UserAccountPasswordLenghtAssembleFullMessage = Resources.USERACCOUNTPASSWORDLENGHT +
                        Resources.USERACCOUNTPASSWORDMINLENGHT.ToString() + " символов";

                /// <summary>
                /// Содержит строку с сообщением и минимальным количеством символов,
                /// которые должны быть в логине
                /// </summary>
                string UserAccountLoginLenghtAssembleFullMessage = Properties.Resources.USERACCOUNTPASSWORDLENGHT +
                       Resources.USERACCOUNTPASSWORDMINLENGHT.ToString() + " символов";

                RuleFor(x => x.Login)
                    .NotEmpty()
                    .WithMessage(Resources.USERACCOUNTLOGINISEMPTY)
                    .NotNull()
                    .WithMessage(Resources.USERACCOUNTLOGINISEMPTY)
                    .MinimumLength(Convert.ToInt32(Resources.USERACCOUNTLOGINMINLENGTH))
                    .WithMessage(UserAccountLoginLenghtAssembleFullMessage);
                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage(Resources.USERACCOUNTPASSWORDISEMPTY)
                    .NotNull()
                    .WithMessage(Resources.USERACCOUNTPASSWORDISEMPTY)
                    .MinimumLength(Convert.ToInt32(Resources.USERACCOUNTPASSWORDMINLENGHT))
                    .WithMessage(UserAccountPasswordLenghtAssembleFullMessage);
            }
        }

        /// <summary>
        /// Задаем условия для валидатора данных о человеке
        /// </summary>
        internal class UserValidatorCheckRequiredFields : AbstractValidator<Common.Models.User.User>
        {
            internal UserValidatorCheckRequiredFields()
            {
                RuleFor(p => p.FirstName)
                    .NotEmpty()
                    .WithMessage(Resources.USERPERSONFIRSTNAMEISEMPTY)
                    .NotNull()
                    .WithMessage(Resources.USERPERSONFIRSTNAMEISEMPTY);
                RuleFor(p => p.LastName)
                    .NotEmpty()
                    .WithMessage(Resources.USERPERSONSECONDNAMEISEMPTY)
                    .NotNull()
                    .WithMessage(Resources.USERPERSONSECONDNAMEISEMPTY);
            }
        }
    }
}
