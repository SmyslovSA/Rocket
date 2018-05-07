using Bogus;
using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о людях,
    /// в моделях хранения данных
    /// </summary>
    public class FakeDbPersonsData
    {
        /// <summary>
        /// Возвращает генератор данных о людях
        /// </summary>
        public Faker<DbPerson> PersonFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных людей
        /// </summary>
        public List<DbPerson> Persons { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о людях
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        public FakeDbPersonsData(int personsCount)
        {
            this.PersonFaker = new Faker<DbPerson>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.FullName, f => f.Person.FullName);

            this.Persons = this.PersonFaker.Generate(personsCount);
        }
    }
}
