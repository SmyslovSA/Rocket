using Bogus;
using System.Collections.Generic;
using System;
using Rocket.DAL.Common.DbModels.Enum;
using Rocket.DAL.Common.DbModels.User;
using System.Linq;

namespace Rocket.BL.Tests.User.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных дополнительных данных о пользователях,
    /// в моделях домена
    /// </summary>
    public class FakeDbUserDetails
    {
        /// <summary>
        /// Возвращает генератор дополнительных данных о пользователях
        /// </summary>
        public Faker<DbUserDetails> UserDetailsFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных дополнительных данных о пользователях
        /// </summary>
        public List<DbUserDetails> UserDetails { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных дополнительных данных о пользователе
        /// </summary>
        /// <param name="userCount">Возвращает количество генерируемых дополнительных данных о пользователях</param>
        public FakeDbUserDetails(int usersCount)
        {
            var result = new Faker<DbUserDetails>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.Language, f => f.PickRandomParam(new FakeDbLanguages(30).Languages.ToArray()))
                .RuleFor(p => p.Sitizenship, f => f.PickRandomParam(new FakeDbCountries(15).Countries.ToArray()))
                .RuleFor(p => p.HowToCall, f => f.PickRandomParam(new FakeDbHowToCalls(3).HowToCalls.ToArray()))
                .RuleFor(p => p.MailAddress, f => { return new FakeDbAddresses(1).Addresses[0]; })
                .RuleFor(p => p.Phones, f => { return new FakePhones(new Random().Next(1, 5)).Phones.ToList(); })
                .RuleFor(p => p.EMailAddresses,
                    f => { return new FakeEmailAddresses(new Random().Next(1, 5)).EmailAddresses.ToList(); })
                .RuleFor(p => p.Gender, f => f.PickRandomParam(Gender.Male, Gender.Female))
                .RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth);

            UserDetails = result.Generate(usersCount);
        }
    }
}