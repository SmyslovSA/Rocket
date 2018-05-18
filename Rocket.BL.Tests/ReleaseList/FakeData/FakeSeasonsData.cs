using Bogus;
using Rocket.BL.Common.Models.ReleaseList;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о сезонах,
    /// в моделях домена
    /// </summary>
    public class FakeSeasonsData
    {
        /// <summary>
        /// Возвращает набор данных о сериях
        /// </summary>
        public FakeEpisodesData FakeEpisodesData { get; }

        /// <summary>
        /// Возвращает генератор данных о сезонах
        /// </summary>
        public Faker<Season> SeasonFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных сезонов
        /// </summary>
        public List<Season> Seasons { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сезонах
        /// </summary>
        public FakeSeasonsData()
        {
            FakeEpisodesData = new FakeEpisodesData();

            SeasonFaker = new Faker<Season>()
                .RuleFor(m => m.Id, f => f.IndexFaker);

            Seasons = new List<Season>();
        }

        /// <summary>
        /// Генерирует и возвращает коллекцию сезонов в заданном количестве
        /// начиная с заданного номера сезона
        /// </summary>
        /// <param name="count">Количество сезонов</param>
        /// <param name="startSeasonNumber">Начальный номер сезона</param>
        /// <returns>Коллекция сезонов</returns>
        public List<Season> Generate(int count, int startSeasonNumber = 1)
        {
            SeasonFaker.RuleFor(m => m.Number, startSeasonNumber++)
                .RuleFor(m => m.ListEpisode, f => FakeEpisodesData.Generate(f.Random.Number(8, 22)));
            var seasons = SeasonFaker.Generate(count);
            Seasons.AddRange(seasons);
            return seasons;
        }
    }
}