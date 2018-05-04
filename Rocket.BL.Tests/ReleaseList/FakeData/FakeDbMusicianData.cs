﻿using System.Collections.Generic;
using Bogus;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Tests.ReleaseList.FakeData
{
	/// <summary>
	/// Представляет набор сгенерированных данных о музыкальных исполнителях,
	/// в моделях хранения данных
	/// </summary>
	public class FakeDbMusicianData
	{
		/// <summary>
		/// Возвращает генератор данных о музыкантах
		/// </summary>
		public Faker<DbMusician> MusicianFaker { get; }

		/// <summary>
		/// Возвращает коллекцию сгенерированных музыкантов
		/// </summary>
		public List<DbMusician> Persons { get; }

		/// <summary>
		/// Создает новый экземпляр сгенерированных данных о музыкантов
		/// </summary>
		/// <param name="personsCount">Необходимое количество сгенерированных музыкантов</param>
		public FakeDbMusicianData(int personsCount)
		{
			this.MusicianFaker = new Faker<DbMusician>()
				.RuleFor(p => p.Id, f => f.IndexFaker)
				.RuleFor(p => p.FullName, f => f.Person.FullName);

			this.Persons = this.MusicianFaker.Generate(personsCount);
		}
	}
}
