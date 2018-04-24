using System.Collections.Generic;

namespace Rocket.BL.Common.Models.ReleaseList
{
	public class MusicBand
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
		/// Возвращает или задает испольнителей музыкального релиза
		/// </summary>
		public ICollection<Musician> Musician { get; set; }
	}
}