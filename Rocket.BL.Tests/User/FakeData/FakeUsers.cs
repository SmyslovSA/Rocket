using Bogus;
using System.Collections.Generic;
using System;
using Rocket.DAL.Common.DbModels;
using System.Linq;

namespace Rocket.BL.Tests.User.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о пользователях,
    /// в моделях домена
    /// </summary>
    public class FakeUsers
    {
        /// <summary>
        /// Возвращает генератор данных о пользователях
        /// </summary>
        public Faker<Common.Models.User.User> UserFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных пользователей
        /// </summary>
        public List<Common.Models.User.User> Users { get; }

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
        public FakeUsers(int usersCount, bool isFirstNameNullOrEmpty, bool isLastNameNullOrEmpty, bool isLoginNullOrEmpty, bool isPasswordNullOrEmpty,  int minLoginLenght, int minPasswordLenght)
        {
            var result = new Faker<Common.Models.User.User>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.AccountStatus, f => f.PickRandomParam((new FakeAccountStatuses(5)).AccountStatuses.ToArray()))
                .RuleFor(p => p.AccountLevel, f => f.PickRandomParam((new FakeAccountLevels(5)).AccountLevels.ToArray()))
                .RuleFor(p => p.Language, f => f.PickRandomParam((new FakeLanguages(30)).Languages.ToArray()))
                .RuleFor(p => p.Sitizenship, f => f.PickRandomParam((new FakeCountries(15)).Countries.ToArray()))
                .RuleFor(p => p.HowToCall, f => f.PickRandomParam((new FakeHowToCalls(3)).HowToCalls.ToArray()))
                .RuleFor(p => p.MailAddress, f => { return (new FakeAddresses(1)).Addresses[0]; })
                .RuleFor(p => p.Phones, f => {  return (new FakePhones((new Random()).Next(1, 5))).Phones.ToList(); })
                .RuleFor(p => p.EMailAddresses, f => { return (new FakeEmailAddresses((new Random()).Next(1, 5))).EmailAddresses.ToList(); })
                .RuleFor(p => p.FirstName, f => { return isFirstNameNullOrEmpty ? string.Empty : f.Person.FirstName; })
                .RuleFor(p => p.LastName, f => { return isLastNameNullOrEmpty ? string.Empty : f.Person.LastName; })
                .RuleFor(p => p.Gender, f => f.PickRandomParam(new Gender[] { Gender.Male, Gender.Female }))
                .RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(p => p.Login, f => { return isLoginNullOrEmpty ? string.Empty : f.Lorem.Letter(minLoginLenght); })
                .RuleFor(p => p.Password, f => { return isPasswordNullOrEmpty ? string.Empty : f.Lorem.Letter(minPasswordLenght); });

            this.Users = result.Generate(usersCount);
        }
    }
}
