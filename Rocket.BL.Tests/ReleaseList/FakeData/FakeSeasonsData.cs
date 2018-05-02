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
        /// Возвращает или задает набор данных о сериях
        /// </summary>
        public FakeEpisodesData FakeEpisodesData { get; set; }

        /// <summary>
        /// Возвращает или задает генератор данных о сезонах
        /// </summary>
        public Faker<Season> SeasonFaker { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных сезонов
        /// </summary>
        public List<Season> Seasons { get; set; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сезонах
        /// </summary>
        public FakeSeasonsData()
        {
            this.FakeEpisodesData = new FakeEpisodesData();

            this.SeasonFaker = new Faker<Season>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.Summary, f => f.Lorem.Text());

            this.Seasons = new List<Season>();
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
            this.SeasonFaker.RuleFor(m => m.Number, startSeasonNumber++)
                .RuleFor(m => m.Episodes, f => this.FakeEpisodesData.Generate(f.Random.Number(8, 22)));
            var seasons = this.SeasonFaker.Generate(count);
            this.Seasons.AddRange(seasons);
            return seasons;
        }
    }
}
