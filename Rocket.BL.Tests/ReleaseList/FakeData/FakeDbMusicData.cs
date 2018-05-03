using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
	/// <summary>
	/// Представляет набор сгенерированных данных о музыке,
	/// в моделях хранения данных
	/// </summary>
	public class FakeDbMusicData
	{

		/// <summary>
		/// Возвращает набор сгенерированных данных о музыкальных треках
		/// </summary>
		public FakeDbMusicTrackData FakeDbMusicTracksData { get; }

		/// <summary>
		/// Возвращает генератор данных о музыкантах
		/// </summary>
		public Faker<DbMusician> MusicianFaker { get; }

		/// <summary>
		/// Возвращает генератор данных о музыкальных жанрах
		/// </summary>
		public Faker<DbMusicGenre> MusicGenreFaker { get; }

		/// <summary>
		/// Возвращает генератор данных о музыкальных релизах
		/// </summary>
		public Faker<DbMusic> MusicFaker { get; }

		/// <summary>
		/// Возвращает коллекцию сгенерированных музыкантов
		/// </summary>
		public List<DbMusician> Musician { get; }


		/// <summary>
		/// Возвращает коллекцию сгенерированных музыкальных жанров
		/// </summary>
		public List<DbMusicGenre> MusicGenre { get; }

		/// <summary>
		/// Возвращает коллекцию сгенерированных музыкальных релизов
		/// </summary>
		public List<DbMusic> Music { get; }

		/// <summary>
		/// Создает новый экземпляр сгенерированных данных о музыкальных релизах
		/// </summary>
		/// <param name="musicianCount">Необходимое количество сгенерированных музыкантов</param>
		/// <param name="genresCount">Необходимое количество сгенерированных музыкальных жанров</param>
		/// <param name="musicCount">Необходимое количество сгенерированных музыкальных релизов</param>
		public FakeDbMusicData(int musicianCount, int genresCount, int musicCount)
		{
			var fakeMusicianData = new FakeDbMusicianData(musicianCount);
			this.MusicianFaker = fakeMusicianData.MusicianFaker;
			this.Musician = fakeMusicianData.Persons;

			this.FakeDbMusicTracksData = new FakeDbMusicTrackData();

			var fakeMusicGenresData = new FakeDbMusicGenreData(genresCount);
			this.MusicGenreFaker = fakeMusicGenresData.MusicGenreFaker;
			this.MusicGenre = fakeMusicGenresData.MusicGenres;

			this.MusicFaker = new Faker<DbMusic>()
				.RuleFor(m => m.Id, f => f.IndexFaker)
				.RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
				.RuleFor(m => m.ReleaseDate, f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(10)))
				.RuleFor(m => m.Genres, f => f.PickRandom(this.MusicGenre, f.Random.Number(1, 2)).ToList())
				.RuleFor(m => m.Musicians, f => f.PickRandom(this.Musician, f.Random.Number(1, 2)).ToList())
				.RuleFor(m => m.Duration, f => f.Date.Timespan(new TimeSpan(4, 0, 0)))
				.RuleFor(m => m.MusicTracks, f => this.FakeDbMusicTracksData.Generate(f.Random.Number(1, 13))); ;

			this.Music = MusicFaker.Generate(musicCount);
		}
	}
}