using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rocket.BL.Common.Models.ReleaseList
{
    public class Music : BaseRelease
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
		public ICollection<MusicGenre> Genres { get; set; } = new Collection<MusicGenre>();

        /// <summary>
        /// Возвращает или задает музыкальные треки которые относятся к релизу
        /// </summary>
        public ICollection<MusicTrack> MusicTracks { get; set; } = new Collection<MusicTrack>();

        /// <summary>
        /// Возвращает или задает исполнителей музыкального релиза
        /// </summary>
        public ICollection<Musician> Musicians { get; set; } = new Collection<Musician>();
    }
}