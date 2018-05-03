using Bogus;
using Rocket.BL.Common.Models.ReleaseList;
using System.Collections.Generic;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
    /// <summary>
    /// Представляет набор сгенерированных данных о жанрах видео,
    /// в моделях домена
    /// </summary>
    public class FakeVideoGenresData
    {
        /// <summary>
        /// Возвращает генератор данных о жанрах видео
        /// </summary>
        public Faker<VideoGenre> VideoGenreFaker { get; }
        
        /// <summary>
        /// Возвращает коллекцию сгенерированных жанров видео
        /// </summary>
        public List<VideoGenre> VideoGenres { get; }
        
        /// <summary>
        /// Создает новый экземпляр сгенерированных данных о жанрах видео
        /// </summary>
        /// <param name="genresCount">Необходимое количество сгенерированных жанров видео</param>
        public FakeVideoGenresData(int genresCount)
        {
            this.VideoGenreFaker = new Faker<VideoGenre>()
                .RuleFor(g => g.Id, f => f.IndexFaker)
                .RuleFor(g => g.Name, f => f.Random.Word());

            this.VideoGenres = this.VideoGenreFaker.Generate(genresCount);
        }
    }
}
