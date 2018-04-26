namespace Rocket.BL.Common.Models.ReleaseList
{
	public class Musician
	{
		/// <summary>
		/// Возвращает или задает уникальный идентификатор музыкального исполнителя
		/// </summary>
		public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает полное имя музыкального исполнителя (название группы)
        /// </summary>
        public string FullName { get; set; }
	}

}