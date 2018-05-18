using Bogus;
using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о людях,
    /// в моделях хранения данных
    /// </summary>
    public class FakePersonEntitiesData
    {
        /// <summary>
        /// Возвращает генератор данных о людях
        /// </summary>
        public Faker<PersonEntity> PersonFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных людей
        /// </summary>
        public List<PersonEntity> Persons { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о людях
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        public FakePersonEntitiesData(int personsCount)
        {
            var nameRu = new Bogus.DataSets.Name("ru");
            PersonFaker = new Faker<PersonEntity>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.FullNameRu, f => nameRu.FullName())
                .RuleFor(p => p.FullNameEn, f => f.Person.FullName);

            Persons = PersonFaker.Generate(personsCount);
        }
    }
}