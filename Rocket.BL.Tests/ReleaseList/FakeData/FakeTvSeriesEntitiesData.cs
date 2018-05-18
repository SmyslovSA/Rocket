using Bogus;
using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Collections.Generic;
using System.Linq;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о сериалах,
    /// в моделях хранения данных
    /// </summary>
    public class FakeTvSeriesEntitiesData
    {
        /// <summary>
        /// Возвращает набор сгенерированных данных о сезонах
        /// </summary>
        public FakeSeasonEntitiesData FakeSeasonEntitiesData { get; }

        /// <summary>
        /// Возвращает генератор данных о людях
        /// </summary>
        public Faker<PersonEntity> PersonFaker { get; }

        /// <summary>
        /// Возвращает генератор данных о жанрах видео
        /// </summary>
        public Faker<GenreEntity> GenreFaker { get; }

        /// <summary>
        /// Возвращает генератор данных о сериалах
        /// </summary>
        public Faker<TvSeriasEntity> TVSeriesFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных людей
        /// </summary>
        public List<PersonEntity> Persons { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных жанров видео
        /// </summary>
        public List<GenreEntity> Genres { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных сериалов
        /// </summary>
        public List<TvSeriasEntity> TVSerials { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сериалах
        /// </summary>
        /// <param name="personsCount">Необходимое количество сгенерированных людей</param>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        /// <param name="tvSerialsCount">Необходимое количество сгенерированных сериалах</param>
        public FakeTvSeriesEntitiesData(int personsCount, int genresCount, int tvSerialsCount)
        {
            FakeSeasonEntitiesData = new FakeSeasonEntitiesData();

            var fakePersonsData = new FakePersonEntitiesData(personsCount);
            PersonFaker = fakePersonsData.PersonFaker;
            Persons = fakePersonsData.Persons;

            var fakeCountriesData = new FakeDbCountriesData(countriesCount);
            CountryFaker = fakeCountriesData.CountryFaker;
            Countries = fakeCountriesData.Countries;

            var fakeVideoGenresData = new FakeDbVideoGenresData(genresCount);
            GenreFaker = fakeVideoGenresData.VideoGenreFaker;
            Genres = fakeVideoGenresData.VideoGenres;

            TVSeriesFaker = new Faker<DbTVSeries>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.Directors, f => f.PickRandom(Persons, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Cast, f => f.PickRandom(Persons, f.Random.Number(2, 12)).ToList())
                .RuleFor(m => m.Genres, f => f.PickRandom(Genres, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Countries, f => f.PickRandom(Countries, f.Random.Number(1, 3)).ToList())
                .RuleFor(m => m.Summary, f => f.Lorem.Text())
                .RuleFor(m => m.DbSeasons, f => FakeDbSeasonsData.Generate(f.Random.Number(1, 13)));

            TVSerials = TVSeriesFaker.Generate(tvSerialsCount);
        }
    }
}