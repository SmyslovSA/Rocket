﻿using System;
using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Описывает модель хранения данных о релизе сериала
    /// </summary>
    public class DbTVSeriesMessage
    {
        /// <summary>
        /// Возвращает или задает идентификационный номер сообщения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер сериала
        /// </summary>
        public int TVSeriesId { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию объектов, содержащих 
        /// сводные данные о получателе и сообщении релизе сериала
        /// </summary>
        public ICollection<ReceiversJoinTVSeries> ReceiversJoinTVSeries { get; set; }

        /// <summary>
        /// Возвращает или задает название сериала
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает порядковый номер сезона
        /// </summary>
        public int SeasonNumber { get; set; }

        /// <summary>
        /// Возвращает или задает номер серии в сезоне
        /// </summary>
        public int EpisodeNumber { get; set; }

        /// <summary>
        /// Возвращает или задает дату выхода серии
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Возвращает или задает время создания сообщения
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}