using Bogus;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о людях,
    /// в моделях домена
    /// </summary>
    public class FakePersonsData
    {
        /// <summary>
        /// Возвращает генератор данных о людях
        /// </summary>
        public Faker<Common.Models.ReleaseList.Person> PersonFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных людей
        /// </summary>
        public List<Common.Models.ReleaseList.Person> Persons { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о людях
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        public FakePersonsData(int personsCount)
        {
            PersonFaker = new Faker<Common.Models.ReleaseList.Person>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.FullNameRu, f => f.Person.FullName);

            Persons = PersonFaker.Generate(personsCount);
        }
    }
}