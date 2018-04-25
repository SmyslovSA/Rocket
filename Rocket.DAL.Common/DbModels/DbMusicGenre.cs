using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels
{
	/// <summary>
	/// Представляет модель хранения данных о музыкальных жанрах
	/// </summary>
	public class DbMusicGenre
	{
		/// <summary>
		/// Возвращает или задает уникальный идентификатор музыкального жанра
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Возвращает или задает название музыкального жанра
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Возвращает или задает коллекцию музыкальных релизов,
		/// которые относятся к этому жанру
		/// </summary>
		public ICollection<DbMusic> DbMusics { get; set; }

	}
}