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
    public class FakeUsersData
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
        public Faker<VideoGenre> VideoGenreFaker { get; }

        /// <summary>
        /// Возвращает генератор данных о фильмах
        /// </summary>
        public Faker<User> UserFaker { get; }

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
        public List<VideoGenre> VideoGenres { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных фильмов
        /// </summary>
        public List<User> Users { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о фильмах
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        /// <param name="filmsCount">Необходимое количество сгенерированных фильмов</param>
        public FakeUsersData(int personsCount, int countriesCount, int genresCount, int filmsCount)
        {
            var fakePersonsData = new FakePersonsData(personsCount);
            this.PersonFaker = fakePersonsData.PersonFaker;
            this.Persons = fakePersonsData.Persons;

            var fakeCountriesData = new FakeCountriesData(countriesCount);
            this.CountryFaker = fakeCountriesData.CountryFaker;
            this.Countries = fakeCountriesData.Countries;

            var fakeVideoGenresData = new FakeVideoGenresData(genresCount);
            this.VideoGenreFaker = fakeVideoGenresData.VideoGenreFaker;
            this.VideoGenres = fakeVideoGenresData.VideoGenres;

            this.UserFaker = new Faker<User>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.ReleaseDate, f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
                .RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.Directors, f => f.PickRandom(this.Persons, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Cast, f => f.PickRandom(this.Persons, f.Random.Number(2, 12)).ToList())
                .RuleFor(m => m.Genres, f => f.PickRandom(this.VideoGenres, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Countries, f => f.PickRandom(this.Countries, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Duration, f => f.Date.Timespan(new TimeSpan(4, 0, 0)))
                .RuleFor(m => m.Summary, f => f.Lorem.Text());

            this.Users = UserFaker.Generate(filmsCount);
        }
    }
}
