using Bogus;
using Rocket.BL.Common.Models.User;
using System.Collections.Generic;

namespace Rocket.BL.Tests.User.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о номерах телефона пользователя,
    /// в моделях домена
    /// </summary>
    public class FakePhones
    {
        /// <summary>
        /// Возвращает генератор данных о номерах телефона пользователя
        /// </summary>
        public Faker<string> PhoneFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных номеров телефонов пользователей 
        /// </summary>
        public List<string> Phones { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных телефонных номеров пользователей
        /// </summary>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        public FakePhones(int phonesCount)
        {
            this.PhoneFaker = new Faker<string>()
                .RuleFor(c => c, f => f.Phone.PhoneNumber());

            this.Phones = this.PhoneFaker.Generate(phonesCount);
        }
    }
}
