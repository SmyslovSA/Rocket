namespace Rocket.BL.Common.Models.ReleaseList
{
	public class MusicGenre
	{
		/// <summary>
		/// Возвращает или задает уникальный идентификатор музыкального жанра
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Возвращает или задает музыкальный жанр
		/// </summary>
		public string Genre { get; set; }
	}
}