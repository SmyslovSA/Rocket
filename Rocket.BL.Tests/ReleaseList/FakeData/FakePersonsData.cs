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
        /// Возвращает или задает генератор данных о людях
        /// </summary>
        public Faker<Common.Models.ReleaseList.Person> PersonFaker { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных людей
        /// </summary>
        public List<Common.Models.ReleaseList.Person> Persons { get; set; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о людях
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        public FakePersonsData(int personsCount)
        {
            this.PersonFaker = new Faker<Common.Models.ReleaseList.Person>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.FullName, f => f.Person.FullName);

            this.Persons = this.PersonFaker.Generate(personsCount);
        }
    }
}
