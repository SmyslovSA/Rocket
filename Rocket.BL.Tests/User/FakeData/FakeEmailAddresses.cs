using Bogus;
using Rocket.BL.Common.Models.User;
using System.Collections.Generic;

namespace Rocket.BL.Tests.User.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных об электронных адресах пользователей,
    /// в моделях домена
    /// </summary>
    public class FakeEmailAddresses
    {
        /// <summary>
        /// Возвращает генератор данных об электронных адресах пользователей
        /// </summary>
        public Faker<string> EmailAddressFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных электронных адресов пользователей
        /// </summary>
        public List<string> EmailAddresses { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных электронных адресов пользователей
        /// </summary>
        /// <param name="emailAddressesCount">Необходимое количество сгенерированных адресов пользователей</param>
        public FakeEmailAddresses(int emailAddressCount)
        {
            this.EmailAddressFaker = new Faker<string>()
                .RuleFor(c => c, f => f.Internet.Email());

            this.EmailAddresses = this.EmailAddressFaker.Generate(emailAddressCount);
        }
    }
}
