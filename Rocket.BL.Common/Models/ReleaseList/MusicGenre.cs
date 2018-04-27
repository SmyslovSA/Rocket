namespace Rocket.BL.Common.Models.ReleaseList
{
	/// <summary>
	/// Представляет информацию о жанре музыкального релиза
	/// </summary>
	public class MusicGenre
	{
		/// <summary>
		/// Возвращает или задает уникальный идентификатор музыкального жанра
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Возвращает или задает название музыкального жанра
		/// </summary>
		public string Name { get; set; }
	}
}