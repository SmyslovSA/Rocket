using Bogus;
using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о странах,
    /// в моделях хранения данных
    /// </summary>
    public class FakeDbCountriesData
    {
        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о странах
        /// </summary>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        public FakeDbCountriesData(int countriesCount)
        {
            CountryFaker = new Faker<DbCountry>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Address.Country());

            Countries = CountryFaker.Generate(countriesCount);
        }

        /// <summary>
        /// Возвращает генератор данных о странах
        /// </summary>
        public Faker<DbCountry> CountryFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных стран
        /// </summary>
        public List<DbCountry> Countries { get; }
    }
}