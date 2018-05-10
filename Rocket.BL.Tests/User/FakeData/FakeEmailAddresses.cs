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
        /// Возвращает коллекцию сгенерированных электронных адресов пользователей
        /// </summary>
        public List<string> EmailAddresses { get; } = new List<string>();

        /// <summary>
        /// Создает новый экземпляр сгенерированных электронных адресов пользователей
        /// </summary>
        /// <param name="emailAddressesCount">Необходимое количество сгенерированных адресов пользователей</param>
        public FakeEmailAddresses(int emailAddressCount)
        {
            var FakerEmailAddress = new Faker();

            this.EmailAddresses.Clear();

            for (int i = 0; i < emailAddressCount; i++)
            {
                this.EmailAddresses.Add(FakerEmailAddress.Internet.Email());
            }
        }
    }
}
