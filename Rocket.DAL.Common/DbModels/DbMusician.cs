using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels
{
	/// <summary>
	/// Представляет модель хранения данных музыкальных исполнителей
	/// </summary>
	public class DbMusician
	{
		/// <summary>
		/// Возвращает или задает уникальный идентификатор музыканта
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Возвращает или задает имя музыканта
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Возвращает или задает фамилию музыканта
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Возвращает или задает псевдоним музыканта
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// Возвращает или задает коллекцию музыкальных групп,
		/// которые относятся к данному исполнителю
		/// </summary>
		public ICollection<DbMusicBand> DbMusicBand { get; set; }

		/// <summary>
		/// Возвращает или задает коллекцию музыкальных релизов,
		/// которые относятся к данному исполнителю
		/// </summary>
		public ICollection<DbMusic> DbMusic { get; set; }
	}
}