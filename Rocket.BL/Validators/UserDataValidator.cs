using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;

namespace Rocket.BL.Validators
{
    /// <summary>
    /// класс для проверок валидации для пользователя
    /// </summary>
    public class UserDataValidator : AbstractValidator<SimpleUser>
    {
        public UserDataValidator()
        {
            RuleFor(x => x.FirstName).NotNull().MinimumLength(2).WithMessage("Name cannot be less than 2 characters");
            RuleFor(x => x.LastName).NotNull().MinimumLength(2)
                .WithMessage("Lastname cannot be less than 2 characters");
            RuleFor(x => x.Avatar).NotNull();
        }
    }
}