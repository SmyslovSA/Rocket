using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels
{
	/// <summary>
	/// Представляет модель хранения данных музыкальных групп
	/// </summary>
	public class DbMusicBand
	{
		/// <summary>
		/// Возвращает или задает уникальный идентификатор музыкальной группы
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Возвращает или задает название музыкальной группы
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Возвращает или задает коллекцию испольнителей данной группы
		/// </summary>
		public ICollection<DbMusician> DbMusician { get; set; }

		/// <summary>
		/// Возвращает или задает коллекцию музыкальных релизов,
		/// которые относятся к данной группе
		/// </summary>
		public ICollection<DbMusic> DbMusic { get; set; }
	}
}