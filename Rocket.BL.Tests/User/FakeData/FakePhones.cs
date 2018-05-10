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
        /// Возвращает коллекцию сгенерированных номеров телефонов пользователей 
        /// </summary>
        public List<string> Phones { get; } = new List<string>();

        /// <summary>
        /// Создает новый экземпляр сгенерированных телефонных номеров пользователей
        /// </summary>
        /// <param name="countriesCount">Необходимое количество сгенерированных телефонных номеров пользователей</param>
        public FakePhones(int phonesCount)
        {
            var FakerPhone = new Faker();

            this.Phones.Clear();

            for (int i = 0; i < phonesCount; i++)
            {
                this.Phones.Add(FakerPhone.Phone.PhoneNumber());
            }
        }
    }
}
