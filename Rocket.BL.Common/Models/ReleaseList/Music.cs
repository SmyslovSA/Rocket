using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models.ReleaseList
{
	public class Music
	{
		/// <summary>
		/// Возвращает или задает уникальный идентификатор музыкального релиза
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Возвращает или задает название музыкального релиза
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Возвращает или задает информацию о выходе музыкального релиза
		/// </summary>
		public Release Release { get; set; }

		/// <summary>
		/// Возвращает или задает относительный путь
		/// от корневой папки приложения к файлу изображения постера музыкального релиза
		/// </summary>
		public string PosterImagePath { get; set; }

		/// <summary>
		/// Возвращает или задает продолжительность музыкального релиза
		/// </summary>
		public TimeSpan Duration { get; set; }

		/// <summary>
		/// Возвращает или задает коллекцию жанров, к которым относится музыкальный релиз
		/// </summary>
		public ICollection<MusicGenre> Genres { get; set; }

		/// <summary>
		/// Возвращает или задает музыкальные треки которые относятся к релизу
		/// </summary>
		public ICollection<MusicTrack> MusicTrack { get; set; }

		/// <summary>
		/// Возвращает или задает группы испольнителей музыкального релиза
		/// </summary>
		public ICollection<MusicBand> MusicBand { get; set; }

		/// <summary>
		/// Возвращает или задает испольнителей музыкального релиза
		/// </summary>
		public ICollection<Musician> Musician { get; set; }
	}
}