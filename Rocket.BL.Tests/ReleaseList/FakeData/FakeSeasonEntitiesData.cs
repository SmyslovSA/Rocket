using Bogus;
using Rocket.DAL.Common.DbModels.Parser;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о сезонах,
    /// в моделях хранения данных
    /// </summary>
    public class FakeSeasonEntitiesData
    {
        /// <summary>
        /// Возвращает набор данных о сериях
        /// </summary>
        public FakeEpisodeEntitiesData FakeEpisodeEntitiesData { get; }

        /// <summary>
        /// Возвращает генератор данных о сезонах
        /// </summary>
        public Faker<SeasonEntity> SeasonFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных сезонов
        /// </summary>
        public List<SeasonEntity> Seasons { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сезонах
        /// </summary>
        public FakeSeasonEntitiesData()
        {
            FakeEpisodeEntitiesData = new FakeEpisodeEntitiesData();

            SeasonFaker = new Faker<SeasonEntity>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.PosterImageUrl, f => f.Internet.Url());

            Seasons = new List<SeasonEntity>();
        }

        /// <summary>
        /// Генерирует и возвращает коллекцию сезонов в заданном количестве
        /// начиная с заданного номера сезона
        /// </summary>
        /// <param name="count">Количество сезонов</param>
        /// <param name="startSeasonNumber">Начальный номер сезона</param>
        /// <returns>Коллекция сезонов</returns>
        public List<SeasonEntity> Generate(int count, int startSeasonNumber = 1)
        {
            SeasonFaker.RuleFor(m => m.Number, startSeasonNumber++)
                .RuleFor(m => m.ListEpisode, f =>
                    {
                        FakeEpisodeEntitiesData.EpisodeFaker
                            .RuleFor(m => m.SeasonId, startSeasonNumber);
                        return FakeEpisodeEntitiesData.Generate(f.Random.Number(8, 22));
                    }
                );
            var seasons = SeasonFaker.Generate(count);
            Seasons.AddRange(seasons);
            return seasons;
        }
    }
}