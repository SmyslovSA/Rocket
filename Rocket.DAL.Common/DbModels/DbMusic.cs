using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.DAL.Common.DbModels
{
	/// <summary>
	/// Представляет модель хранения данных музыкального релиза
	/// </summary>
	public class DbMusic
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
        /// Возвращает или задает дату выхода музыкального релиза
        /// </summary>
        public DateTime ReleaseDate { get; set; }

		/// <summary>
		/// Возвращает или задает относительный путь
		/// от корневой папки приложения к файлу изображения постера музыкального релиза
		/// </summary>
		public string PosterImagePath { get; set; }

		/// <summary>
		/// Возвращает или задает продолжительность музыкального релиза
		/// </summary>
		public int Duration { get; set; }

		/// <summary>
		/// Возвращает или задает коллекцию жанров, к которым относится музыкальный релиз
		/// </summary>
		public ICollection<DbMusicGenre> Genres { get; set; }

		/// <summary>
		/// Возвращает или задает музыкальные треки которые относятся к релизу
		/// </summary>
		public ICollection<DbMusicTrack> MusicTracks { get; set; }

		/// <summary>
		/// Возвращает или задает исполнителей музыкального релиза
		/// </summary>
		public ICollection<DbMusician> Musicians { get; set; }

	    /// <summary>
	    /// Исполнитель
	    /// </summary>
	    public string Artist { get; set; }

	    /// <summary>
	    /// Тип релиза
	    /// </summary>
	    public string Type { get; set; }

        /// <summary>
        /// Источники релиза
        /// </summary>
        public ICollection<ResourceItemEntity> ResourceItems { get; set; }
    }
}
