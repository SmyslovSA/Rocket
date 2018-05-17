﻿using Bogus;
using Rocket.DAL.Common.DbModels;
using System;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о сериях,
    /// в моделях хранения данных
    /// </summary>
    public class FakeDbEpisodesData
    {
        /// <summary>
        /// Возвращает генератор данных о сериях
        /// </summary>
        public Faker<DbEpisode> EpisodeFaker { get; }

        /// <summary>
        /// Возвращает коллекцию сгенерированных серий
        /// </summary>
        public List<DbEpisode> Episodes { get; }

        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о сериях
        /// </summary>
        public FakeDbEpisodesData()
        {
            this.EpisodeFaker = new Faker<DbEpisode>()
                .RuleFor(m => m.Id, f => f.IndexFaker)
                .RuleFor(m => m.ReleaseDate,
                    f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
                .RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
                //.RuleFor(m => m.DurationInMinutes, f => f.Date.))
                .RuleFor(m => m.Summary, f => f.Lorem.Text());

            this.Episodes = new List<DbEpisode>();
        }

        /// <summary>
        /// Генерирует и возвращает коллекцию серий в заданном количестве
        /// начиная с заданного номера серии
        /// </summary>
        /// <param name="count">Количество серий</param>
        /// <param name="startEpisodeNumber">Начальный номер серии</param>
        /// <returns>Коллекция серий</returns>
        public List<DbEpisode> Generate(int count, int startEpisodeNumber = 1)
        {
            this.EpisodeFaker.RuleFor(m => m.Number, startEpisodeNumber++);
            var episodes = this.EpisodeFaker.Generate(count);
            this.Episodes.AddRange(episodes);
            return episodes;
        }
    }
}
