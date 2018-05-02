using Bogus;
using Rocket.DAL.Common.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о фильмах,
    /// в моделях хранения данных
    /// </summary>
    public class FakeDbFilmsData
    {
        /// <summary>
        /// Возвращает или задает генератор данных о людях
        /// </summary>
        public Faker<DbPerson> PersonFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о странах
        /// </summary>
        public Faker<DbCountry> CountryFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о жанрах видео
        /// </summary>
        public Faker<DbVideoGenre> VideoGenreFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о фильмах
        /// </summary>
        public Faker<DbFilm> FilmFaker { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных людей
        /// </summary>
        public List<DbPerson> Persons { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных стран
        /// </summary>
        public List<DbCountry> Countries { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных жанров видео
        /// </summary>
        public List<DbVideoGenre> VideoGenres { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных фильмов
        /// </summary>
        public List<DbFilm> Films { get; set; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о фильмах
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        /// <param name="filmsCount">Необходимое количество сгенерированных фильмов</param>
        public FakeDbFilmsData(int personsCount, int countriesCount, int genresCount, int filmsCount)
        {
            var fakePersonsData = new FakeDbPersonsData(personsCount);
            this.PersonFaker = fakePersonsData.PersonFaker;
            this.Persons = fakePersonsData.Persons;

            var fakeCountriesData = new FakeDbCountriesData(countriesCount);
            this.CountryFaker = fakeCountriesData.CountryFaker;
            this.Countries = fakeCountriesData.Countries;

            var fakeVideoGenresData = new FakeDbVideoGenresData(genresCount);
            this.VideoGenreFaker = fakeVideoGenresData.VideoGenreFaker;
            this.VideoGenres = fakeVideoGenresData.VideoGenres;

            this.FilmFaker = new Faker<DbFilm>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.ReleaseDate, f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
                .RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.Directors, f => f.PickRandom(this.Persons, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Cast, f => f.PickRandom(this.Persons, f.Random.Number(2, 12)).ToList())
                .RuleFor(m => m.Genres, f => f.PickRandom(this.VideoGenres, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Countries, f => f.PickRandom(this.Countries, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Duration, f => f.Date.Timespan(new TimeSpan(4, 0, 0)))
                .RuleFor(m => m.Summary, f => f.Lorem.Text());

            this.Films = FilmFaker.Generate(filmsCount);
        }
    }
}
