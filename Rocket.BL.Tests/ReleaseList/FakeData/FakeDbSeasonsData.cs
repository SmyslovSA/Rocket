using Bogus;
using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о сезонах,
    /// в моделях хранения данных
    /// </summary>
    public class FakeDbSeasonsData
    {
        /// <summary>
        /// Возвращает набор данных о сериях
        /// </summary>
        public FakeDbEpisodesData FakeDbEpisodesData { get; }

        /// <summary>
        /// Возвращает генератор данных о сезонах
        /// </summary>
        public Faker<DbSeason> SeasonFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных сезонов
        /// </summary>
        public List<DbSeason> Seasons { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сезонах
        /// </summary>
        public FakeDbSeasonsData()
        {
            this.FakeDbEpisodesData = new FakeDbEpisodesData();

            this.SeasonFaker = new Faker<DbSeason>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.Summary, f => f.Lorem.Text());

            this.Seasons = new List<DbSeason>();
        }

        /// <summary>
        /// Генерирует и возвращает коллекцию сезонов в заданном количестве
        /// начиная с заданного номера сезона
        /// </summary>
        /// <param name="count">Количество сезонов</param>
        /// <param name="startSeasonNumber">Начальный номер сезона</param>
        /// <returns>Коллекция сезонов</returns>
        public List<DbSeason> Generate(int count, int startSeasonNumber = 1)
        {
            this.SeasonFaker.RuleFor(m => m.Number, startSeasonNumber++)
                .RuleFor(m => m.Episodes, f => this.FakeDbEpisodesData.Generate(f.Random.Number(8, 22)));
            var seasons = this.SeasonFaker.Generate(count);
            this.Seasons.AddRange(seasons);
            return seasons;
        }
    }
}
