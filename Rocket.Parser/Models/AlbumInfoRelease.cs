﻿namespace Rocket.Parser.Models
{
    /// <summary>
    /// Релиз музыки на сайте album-info.ru
    /// </summary>
    public class AlbumInfoRelease
    {
        /// <summary>
        /// Id внутри ресурса
        /// </summary>
        public string ResourceInternalId { get; set; }

        /// <summary>
        /// Id элемента ресурса
        /// </summary>
        public int ResourceItemId { get; set; }

        /// <summary>
        /// Название релиза
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата релиза
        /// </summary>
        public string Date { get; set; }


        /// <summary>
        /// URL изображение релиза
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Жанр релиза
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Список треков
        /// </summary>
        public string TrackList { get; set; }

    }
}
