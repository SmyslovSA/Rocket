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
        public FakeSeasonsData FakeSeasonsData { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о людях
        /// </summary>
        public Faker<Common.Models.ReleaseList.Person> PersonFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о странах
        /// </summary>
        public Faker<Country> CountryFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о жанрах видео
        /// </summary>
        public Faker<VideoGenre> VideoGenreFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о сериалах
        /// </summary>
        public Faker<TVSeries> TVSeriesFaker { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных людей
        /// </summary>
        public List<Common.Models.ReleaseList.Person> Persons { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных стран
        /// </summary>
        public List<Country> Countries { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных жанров видео
        /// </summary>
        public List<VideoGenre> VideoGenres { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных сериалов
        /// </summary>
        public List<TVSeries> TVSerials { get; set; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сериалах
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        /// <param name="tvSerialsCount">Необходимое количество сгенерированных сериалах</param>
        public FakeTVSerialsData(int personsCount, int countriesCount, int genresCount, int tvSerialsCount)
        {
            this.FakeSeasonsData = new FakeSeasonsData();

            var fakePersonsData = new FakePersonsData(personsCount);
            this.PersonFaker = fakePersonsData.PersonFaker;
            this.Persons = fakePersonsData.Persons;

            var fakeCountriesData = new FakeCountriesData(countriesCount);
            this.CountryFaker = fakeCountriesData.CountryFaker;
            this.Countries = fakeCountriesData.Countries;

            var fakeVideoGenresData = new FakeVideoGenresData(genresCount);
            this.VideoGenreFaker = fakeVideoGenresData.VideoGenreFaker;
            this.VideoGenres = fakeVideoGenresData.VideoGenres;

            this.TVSeriesFaker = new Faker<TVSeries>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.Directors, f => f.PickRandom(this.Persons, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Cast, f => f.PickRandom(this.Persons, f.Random.Number(2, 12)).ToList())
                .RuleFor(m => m.Genres, f => f.PickRandom(this.VideoGenres, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Countries, f => f.PickRandom(this.Countries, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Summary, f => f.Lorem.Text())
                .RuleFor(m => m.Seasons, f => this.FakeSeasonsData.Generate(f.Random.Number(1, 13)));

            this.TVSerials = TVSeriesFaker.Generate(tvSerialsCount);
        }
    }
}
