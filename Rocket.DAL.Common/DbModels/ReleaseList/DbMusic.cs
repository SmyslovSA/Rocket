using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rocket.DAL.Common.DbModels.ReleaseList
{
    /// <summary>
    /// Представляет модель хранения данных музыкального релиза
    /// </summary>
    public class DbMusic : DbBaseRelease
	{
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
        public virtual ICollection<DbMusicGenre> Genres { get; set; } = new Collection<DbMusicGenre>();

        /// <summary>
        /// Возвращает или задает музыкальные треки которые относятся к релизу
        /// </summary>
        public virtual ICollection<DbMusicTrack> MusicTracks { get; set; } = new Collection<DbMusicTrack>();

        /// <summary>
        /// Возвращает или задает исполнителей музыкального релиза
        /// </summary>
        public virtual ICollection<DbMusician> Musicians { get; set; } = new Collection<DbMusician>();
	}
}
