using System;
using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.Parser.Models
{
    /// <summary>
    /// Модель для временной агрегации данных результата парсинга (нужна чтобы потом сделать дополнительный парсинг и вставку в бд)
    /// </summary>
    internal class LostfilmSerialModel
    {
        /// <summary>
        /// Дополнительная ссылка для получения деталей по сериалу.
        /// </summary>
        public string AddUrlForDetail { get; set; }

        public string NewSeriaDetailUrl { get; set; }

        public string OfficialSiteUrl { get; set; }

        public string ImageUrlTvSerialThumb { get; set; }

        public string TvSerialNameRu { get; set; }

        public string TvSerialNameEn { get; set; }

        public string TvSerialCurrentStatus { get; set; }

        public string TvSerialYearStart { get; set; }

        public string TvSerialCanal { get; set; }

        public string ListGenreForParse { get; set; }

        public double TvSerialRateOnLostFilm { get; set; }

        public string PremiereDateText { get; set; }

        public double RateImDb { get; set; }

        public string EpisodeAndSeriaNumberText { get; set; }
        
        public DateTime DateReleaseRu { get; set; }

        public DateTime DateReleaseEn { get; set; }

        public int DurationInMin { get; set; }

        public ICollection<PersonEntity> ListActor { get; set; }

        public ICollection<PersonEntity> ListDirector { get; set; }

        public ICollection<PersonEntity> ListProducer { get; set; }

        public ICollection<PersonEntity> ListWriter { get; set; }
    }

}
