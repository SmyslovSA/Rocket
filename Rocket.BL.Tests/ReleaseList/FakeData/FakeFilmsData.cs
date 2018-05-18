using Bogus;
using Rocket.BL.Common.Models.ReleaseList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о фильмах,
    /// в моделях домена
    /// </summary>
    public class FakeFilmsData
    {
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
        /// Возвращает генератор данных о фильмах
        /// </summary>
        public Faker<Film> FilmFaker { get; }

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
        /// Возвращает коллекцию сгенерированных фильмов
        /// </summary>
        public List<Film> Films { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о фильмах
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        /// <param name="filmsCount">Необходимое количество сгенерированных фильмов</param>
        public FakeFilmsData(int personsCount, int countriesCount, int genresCount, int filmsCount)
        {
            var fakePersonsData = new FakePersonsData(personsCount);
            PersonFaker = fakePersonsData.PersonFaker;
            Persons = fakePersonsData.Persons;

            var fakeCountriesData = new FakeCountriesData(countriesCount);
            CountryFaker = fakeCountriesData.CountryFaker;
            Countries = fakeCountriesData.Countries;

            var fakeVideoGenresData = new FakeVideoGenresData(genresCount);
            VideoGenreFaker = fakeVideoGenresData.VideoGenreFaker;
            VideoGenres = fakeVideoGenresData.VideoGenres;

            FilmFaker = new Faker<Film>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.ReleaseDate,
                    f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
                .RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.Directors, f => f.PickRandom(Persons, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Cast, f => f.PickRandom(Persons, f.Random.Number(2, 12)).ToList())
                .RuleFor(m => m.Genres, f => f.PickRandom(VideoGenres, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Countries, f => f.PickRandom(Countries, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Duration, f => f.Date.Timespan(new TimeSpan(4, 0, 0)))
                .RuleFor(m => m.Summary, f => f.Lorem.Text());

            Films = FilmFaker.Generate(filmsCount);
        }
    }
}