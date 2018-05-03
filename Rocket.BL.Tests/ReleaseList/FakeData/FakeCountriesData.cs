using Bogus;
using Rocket.BL.Common.Models.ReleaseList;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о странах,
    /// в моделях домена
    /// </summary>
    public class FakeCountriesData
    {
        /// <summary>
        /// Возвращает генератор данных о странах
        /// </summary>
        public Faker<Country> CountryFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных стран
        /// </summary>
        public List<Country> Countries { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о странах
        /// </summary>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        public FakeCountriesData(int countriesCount)
        {
            this.CountryFaker = new Faker<Country>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Address.Country());

            this.Countries = this.CountryFaker.Generate(countriesCount);
        }
    }
}
