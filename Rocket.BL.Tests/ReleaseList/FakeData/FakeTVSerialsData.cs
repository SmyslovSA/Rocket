using Bogus;
using Rocket.BL.Common.Models.ReleaseList;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о сериалах,
    /// в моделях домена
    /// </summary>
    public class FakeTVSerialsData
    {
        /// <summary>
        /// Возвращает набор сгенерированных данных о сезонах
        /// </summary>
        public FakeSeasonsData FakeSeasonsData { get; }

        /// <summary>
        /// Возвращает генератор данных о людях
        /// </summary>
        public Faker<Common.Models.ReleaseList.Person> PersonFaker { get; }

        /// <summary>
        /// Возвращает генератор данных о странах
        /// </summary>
        public Faker<Country> CountryFaker { get; }

        /// <summary>
        /// Возвращает генератор данных о жанрах видео
        /// </summary>
        public Faker<Genre> VideoGenreFaker { get; }

        /// <summary>
        /// Возвращает генератор данных о сериалах
        /// </summary>
        public Faker<TVSeries> TVSeriesFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных людей
        /// </summary>
        public List<Common.Models.ReleaseList.Person> Persons { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных стран
        /// </summary>
        public List<Country> Countries { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных жанров видео
        /// </summary>
        public List<Genre> VideoGenres { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных сериалов
        /// </summary>
        public List<TVSeries> TVSerials { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сериалах
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        /// <param name="tvSerialsCount">Необходимое количество сгенерированных сериалах</param>
        public FakeTVSerialsData(int personsCount, int countriesCount, int genresCount, int tvSerialsCount)
        {
            FakeSeasonsData = new FakeSeasonsData();

            var fakePersonsData = new FakePersonsData(personsCount);
            PersonFaker = fakePersonsData.PersonFaker;
            Persons = fakePersonsData.Persons;

            var fakeCountriesData = new FakeCountriesData(countriesCount);
            CountryFaker = fakeCountriesData.CountryFaker;
            Countries = fakeCountriesData.Countries;

            var fakeVideoGenresData = new FakeVideoGenresData(genresCount);
            VideoGenreFaker = fakeVideoGenresData.VideoGenreFaker;
            VideoGenres = fakeVideoGenresData.VideoGenres;

            TVSeriesFaker = new Faker<TVSeries>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.TitleRu, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.ListPerson, f => f.PickRandom(Persons, f.Random.Number(2, 12)).ToList())
                .RuleFor(m => m.Genres, f => f.PickRandom(VideoGenres, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Summary, f => f.Lorem.Text())
                .RuleFor(m => m.ListSeasons, f => FakeSeasonsData.Generate(f.Random.Number(1, 13)));

            TVSerials = TVSeriesFaker.Generate(tvSerialsCount);
        }
    }
}