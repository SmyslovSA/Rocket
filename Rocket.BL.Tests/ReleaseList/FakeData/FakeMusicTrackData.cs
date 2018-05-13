using System;
using System.Collections.Generic;
using Bogus;
using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
	/// <summary>
	/// Представляет набор сгенерированных данных о музыкальных треках,
	/// в моделях домена
	/// </summary>
	public class FakeMusicTrackData
	{
		/// <summary>
		/// Возвращает генератор данных о музыкальных треках
		/// </summary>
		public Faker<MusicTrack> MusicTrackFaker { get; }

		/// <summary>
		/// Возвращает коллекцию сгенерированных музыкальных треках
		/// </summary>
		public List<MusicTrack> MusicTrack { get; }

		/// <summary>
		/// Создает новый экземпляр сгенерированных данных о музыкальных треках
		/// </summary>
		public FakeMusicTrackData()
		{
			this.MusicTrackFaker = new Faker<MusicTrack>()
				.RuleFor(m => m.Id, f => f.IndexFaker)
				.RuleFor(m => m.Title, f => string.Join(" ", f.Lorem.Words(2)))
				.RuleFor(m => m.Duration, f => f.Date.Timespan(new TimeSpan(1, 0, 0)));

			this.MusicTrack = new List<MusicTrack>();
		}

		/// <summary>
		/// Генерирует и возвращает коллекцию музыкальных треков в заданном количестве
		/// начиная с заданного номера музыкального трека
		/// </summary>
		/// <param name="count">Количество музыкальных треков</param>
		/// <param name="startMusicTrackNumber">Начальный номер музыкального трека</param>
		/// <returns>Коллекция музыкальных треков</returns>
		public List<MusicTrack> Generate(int count, int startMusicTrackNumber = 1)
		{
			var musicTrack = this.MusicTrackFaker.Generate(count);
			this.MusicTrack.AddRange(musicTrack);
			return musicTrack;
		}
	}
}
