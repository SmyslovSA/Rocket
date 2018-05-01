using System;

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

        /// <summary>
        /// Ссылка на изображение-миниатюру для сериала.
        /// </summary>
        public string ImageUrlTvSerialThumb { get; set; }

        /// <summary>
        /// Название сериала по-русски.
        /// </summary>
        public string TvSerialNameRu { get; set; }

        /// <summary>
        /// Название сериала по-английски.
        /// </summary>
        public string TvSerialNameEn { get; set; }

        /// <summary>
        /// Текущий статус сериала.
        /// </summary>
        public string TvSerialCurrentStatus { get; set; }

        /// <summary>
        /// Год начала показа сериала.
        /// </summary>
        public string TvSerialYearStart { get; set; }

        /// <summary>
        /// Теливизионный канал на котором показывают сериал.
        /// </summary>
        public string TvSerialCanal { get; set; }

        /// <summary>
        /// Список жанров в виде строки для последующего парсинга.
        /// </summary>
        public string ListGenreForParse { get; set; }

        /// <summary>
        /// Рейтинг сериала на Lostfilm.
        /// </summary>
        public double TvSerialRateOnLostFilm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PremiereDateText { get; set; }

        public double RateImDb { get; set; }

        public string OfficialSite { get; set; }

        public string EpisodeAndSeriaNumberText { get; set; }

        public string NewSeriaDetailNewUrl { get; set; }

        public DateTime DateReleaseRu { get; set; }

        public DateTime DateReleaseEn { get; set; }

        public double DurationInMin { get; set; }
    }

}
