using Bogus;
using Rocket.BL.Common.Models.ReleaseList;
using System;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о сериях,
    /// в моделях домена
    /// </summary>
    public class FakeEpisodesData
    {
        /// <summary>
        /// Возвращает генератор данных о сериях
        /// </summary>
        public Faker<Episode> EpisodeFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных серий
        /// </summary>
        public List<Episode> Episodes { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сериях
        /// </summary>
        public FakeEpisodesData()
        {
            EpisodeFaker = new Faker<Episode>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(
                    m => m.ReleaseDate,
                    f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
                .RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
                .RuleFor(m => m.Duration, f => f.Date.Timespan(new TimeSpan(1, 0, 0)))
                .RuleFor(m => m.Summary, f => f.Lorem.Text());

            Episodes = new List<Episode>();
        }

        /// <summary>
        /// Генерирует и возвращает коллекцию серий в заданном количестве
        /// начиная с заданного номера серии
        /// </summary>
        /// <param name="count">Количество серий</param>
        /// <param name="startEpisodeNumber">Начальный номер серии</param>
        /// <returns>Коллекция серий</returns>
        public List<Episode> Generate(int count, int startEpisodeNumber = 1)
        {
            EpisodeFaker.RuleFor(m => m.Number, startEpisodeNumber++);
            var episodes = EpisodeFaker.Generate(count);
            Episodes.AddRange(episodes);
            return episodes;
        }
    }
}