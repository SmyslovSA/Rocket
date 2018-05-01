using System;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.Parser.Models
{
    /// <summary>
    /// Модель для временной агрегации данных результата парсинга (нужна чтобы потом сделать дополнительный парсинг и вставку в бд)
    /// </summary>
    internal class LostfilmSerialModel
    {
        /// <summary>
        /// Существует в бд.
        /// </summary>
        public bool IsExist { get; set; }

        /// <summary>
        /// Список жанров в виде строки для последующего парсинга.
        /// </summary>
        public string ListGenreForParse { get; set; }

        /// <summary>
        /// Дата премьеры прописью для последующего парсинга.
        /// </summary>
        public string PremiereDateForParse { get; set; }

        public string NewSeriaDetailNewUrl { get; set; }


        public TvSeriasEntity TvSeriasEntity { get; set; }
        
    }

}
