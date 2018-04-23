namespace Rocket.BL.Common.Models.ReleaseList
{
	public class Musician
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
	}

}