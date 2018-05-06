using Bogus;
using System.Collections.Generic;

namespace Rocket.BL.Tests.User.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о людях,
    /// в моделях домена
    /// </summary>
    public class FakeUser
    {
        /// <summary>
        /// Возвращает генератор данных о пользователях
        /// </summary>
        public Faker<Common.Models.User.User> UserFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных пользователей
        /// </summary>
        public List<Common.Models.User.User> UsersFaker { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о пользователях
        /// </summary>
        /// <param name="userCount">Возвращает количество генерируемых пользователей</param>
        /// <param name="isFirstNameNullOrEmpty">Возвращает true, если имя пользователя не указано</param>
        /// <param name="isLastNameNullOrEmpty">Возвращает true, если фамилия пользователя не указана</param>667.южж
        /// <param name="isLoginNullOrEmpty">Возвращает true, если логин не указан</param>
        /// <param name="isPasswordNullOrEmpty">Возвращает true, если пароль не указан</param>
        /// <param name="minLoginLenght">Задает минимальное количество символов в логине</param>
        /// <param name="minPasswordLenght">Задает минимальное количество символов в пароле</param>
        public FakeUser(int usersCount, bool isFirstNameNullOrEmpty, bool isLastNameNullOrEmpty, bool isLoginNullOrEmpty, bool isPasswordNullOrEmpty,  int minLoginLenght, int minPasswordLenght)
        {
            var result = new Faker<Common.Models.User.User>()
                .RuleFor(p => p.FirstName, f => { return isFirstNameNullOrEmpty ? string.Empty : f.Person.FirstName; })
                .RuleFor(p => p.LastName, f => { return isLastNameNullOrEmpty ? string.Empty : f.Person.LastName; })
                .RuleFor(p => p.Login, f => { return isLoginNullOrEmpty ? string.Empty : f.Lorem.Letter(minLoginLenght); })
                .RuleFor(p => p.Password, f => { return isPasswordNullOrEmpty ? string.Empty : f.Lorem.Letter(minPasswordLenght); });

            this.UsersFaker = result.Generate(usersCount);
        }
    }
}
