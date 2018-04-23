﻿using System;
using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels
{
	/// <summary>
	/// Представляет модель хранения данных о треках музыкального релиза
	/// </summary>
	public class DbMusicTrack
	{
		/// <summary>
		/// Возвращает или задает уникальный идентификатор трека
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Возвращает или задает номер трека
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// Возвращает или задает название музыкального трека
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Возвращает или задает продолжительность музыкального трека
		/// </summary>
		public TimeSpan Duration { get; set; }

		/// <summary>
		/// Возвращает или задает коллекцию музыкальных релизов,
		/// которые относятся к этому треку
		/// </summary>
		public ICollection<DbMusic> DbMusic { get; set; }
	}
}