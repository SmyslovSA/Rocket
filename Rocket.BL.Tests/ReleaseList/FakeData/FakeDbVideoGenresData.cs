﻿using Bogus;
using Rocket.DAL.Common.DbModels;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о жанрах видео,
    /// в моделях хранения данных
    /// </summary>
    public class FakeDbVideoGenresData
    {
        /// <summary>
        /// Возвращает или задает генератор данных о жанрах видео
        /// </summary>
        public Faker<DbVideoGenre> VideoGenreFaker { get; set; }
        
        /// <summary>
        /// Возвращает или задает коллекцию сгенерированных жанров видео
        /// </summary>
        public List<DbVideoGenre> VideoGenres { get; set; }
        
        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о жанрах видео
        /// </summary>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        public FakeDbVideoGenresData(int genresCount)
        {
            this.VideoGenreFaker = new Faker<DbVideoGenre>()
                .RuleFor(g => g.Id, f => f.IndexFaker)
                .RuleFor(g => g.Name, f => f.Random.Word());

            this.VideoGenres = this.VideoGenreFaker.Generate(genresCount);
        }
    }
}
