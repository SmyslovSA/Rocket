using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Properties;

namespace Rocket.BL.Validators
{
    /// <summary>
    /// Класс для проверок валидации для пользователя.
    /// </summary>
    public class UserDataValidator : AbstractValidator<SimpleUser>
    {
        public UserDataValidator()
        {
            RuleFor(x => x.FirstName).NotNull().MinimumLength(2).WithMessage(Resources.UserRuleForFirstName);
            RuleFor(x => x.LastName).NotNull().MinimumLength(2)
                .WithMessage(Resources.UserRuleForLastName);
            RuleFor(x => x.Avatar).NotNull();
        }
    }
}