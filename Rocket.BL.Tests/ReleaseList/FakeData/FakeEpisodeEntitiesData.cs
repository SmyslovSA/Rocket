using Bogus;
using Rocket.DAL.Common.DbModels.Parser;
using System;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о сериях,
    /// в моделях хранения данных
    /// </summary>
    public class FakeEpisodeEntitiesData
    {
        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сериях
        /// </summary>
        public FakeEpisodeEntitiesData()
        {
            var loremRu = new Bogus.DataSets.Lorem("ru");
            EpisodeFaker = new Faker<EpisodeEntity>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.ReleaseDateRu,
                    f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
                .RuleFor(m => m.ReleaseDateEn,
                    f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
                .RuleFor(m => m.TitleRu, f => string.Join(" ", loremRu.Words(2)))
                .RuleFor(m => m.TitleEn, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.DurationInMinutes, f => f.Random.Number(15, 60))
                .RuleFor(m => m.UrlForEpisodeSource, f => f.Internet.Url());

            Episodes = new List<EpisodeEntity>();
        }

        /// <summary>
        /// Возвращает генератор данных о сериях
        /// </summary>
        public Faker<EpisodeEntity> EpisodeFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных серий
        /// </summary>
        public List<EpisodeEntity> Episodes { get; }

        /// <summary>
        /// Генерирует и возвращает коллекцию серий в заданном количестве
        /// начиная с заданного номера серии
        /// </summary>
        /// <param name="count">Количество серий</param>
        /// <param name="startEpisodeNumber">Начальный номер серии</param>
        /// <returns>Коллекция серий</returns>
        public List<EpisodeEntity> Generate(int count, int startEpisodeNumber = 1)
        {
            EpisodeFaker.RuleFor(m => m.Number, startEpisodeNumber++);
            var episodes = EpisodeFaker.Generate(count);
            Episodes.AddRange(episodes);
            return episodes;
        }
    }
}