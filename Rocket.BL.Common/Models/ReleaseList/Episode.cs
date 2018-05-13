﻿using System;

namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Представляет информацию о конкретной серии сериала
    /// </summary>
    public class Episode : BaseRelease
    {
        /// <summary>
        /// Возвращает или задает номер серии в сезоне
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Возвращает или задает продолжительность серии
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Возвращает или задает краткое описание серии
        /// </summary>
        public string Summary { get; set; }
    }
}