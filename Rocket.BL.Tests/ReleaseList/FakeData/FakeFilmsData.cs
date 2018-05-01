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
        /// Возвращает или задает генератор данных о фильмах
        /// </summary>
        public Faker<Film> FilmFaker { get; set; }

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
        /// Возвращает или задает коллекцию сгенерированных фильмов
        /// </summary>
        public List<Film> Films { get; set; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о фильмах
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        /// <param name="countriesCount">Необходимое количество сгенерированных стран</param>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        /// <param name="filmsCount">Необходимое количество сгенерированных фильмов</param>
        public FakeFilmsData(int personsCount, int countriesCount, int genresCount, int filmsCount)
        {
            this.PersonFaker = new Faker<Common.Models.ReleaseList.Person>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.FullName, f => f.Person.FullName);

            this.Persons = this.PersonFaker.Generate(personsCount);

            this.CountryFaker = new Faker<Country>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Address.Country());

            this.Countries = this.CountryFaker.Generate(countriesCount);

            this.VideoGenreFaker = new Faker<VideoGenre>()
                .RuleFor(g => g.Id, f => f.IndexFaker)
                .RuleFor(g => g.Name, f => f.Random.Word());

            this.VideoGenres = this.VideoGenreFaker.Generate(genresCount);

            this.FilmFaker = new Faker<Film>()
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
