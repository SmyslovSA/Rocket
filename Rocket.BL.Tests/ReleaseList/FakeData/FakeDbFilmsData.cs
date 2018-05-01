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
        public Faker<DbPerson> DbPersonFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о странах
        /// </summary>
        public Faker<DbCountry> DbCountryFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о жанрах видео
        /// </summary>
        public Faker<DbVideoGenre> DbVideoGenreFaker { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о фильмах
        /// </summary>
        public Faker<DbFilm> DbFilmFaker { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных людей
        /// </summary>
        public List<DbPerson> DbPersons { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных стран
        /// </summary>
        public List<DbCountry> DbCountries { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных жанров видео
        /// </summary>
        public List<DbVideoGenre> DbVideoGenres { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных фильмов
        /// </summary>
        public List<DbFilm> DbFilms { get; set; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о фильмах
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        /// <param name="filmsCount">Необходимое количество сгенерированных фильмов</param>
        public FakeDbFilmsData(int personsCount, int countriesCount, int genresCount, int filmsCount)
        {
            this.DbPersonFaker = new Faker<DbPerson>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.FullName, f => f.Person.FullName);

            this.DbPersons = this.DbPersonFaker.Generate(personsCount);

            this.DbCountryFaker = new Faker<DbCountry>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Address.Country());

            this.DbCountries = this.DbCountryFaker.Generate(countriesCount);

            this.DbVideoGenreFaker = new Faker<DbVideoGenre>()
                .RuleFor(g => g.Id, f => f.IndexFaker)
                .RuleFor(g => g.Name, f => f.Random.Word());

            this.DbVideoGenres = this.DbVideoGenreFaker.Generate(genresCount);

            this.DbFilmFaker = new Faker<DbFilm>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.ReleaseDate, f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
                .RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.Directors, f => f.PickRandom(this.DbPersons, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Cast, f => f.PickRandom(this.DbPersons, f.Random.Number(2, 12)).ToList())
                .RuleFor(m => m.Genres, f => f.PickRandom(this.DbVideoGenres, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Countries, f => f.PickRandom(this.DbCountries, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Duration, f => f.Date.Timespan(new TimeSpan(4, 0, 0)))
                .RuleFor(m => m.Summary, f => f.Lorem.Text());

            this.DbFilms = DbFilmFaker.Generate(filmsCount);
        }
    }
}
