using Rocket.BL.Common.Models.ReleaseList;
using System;
using System.Linq.Expressions;
using Rocket.BL.Common.Models.Pagination;

namespace Rocket.BL.Common.Services.ReleaseList
{
    /// <summary>
    /// Представляет сервис для работы с детальной информацией
    /// о сериалах в хранилище данных
    /// </summary>
    public interface ITvSeriesDetailedInfoService : IDisposable
    {
        /// <summary>
        /// Возвращает сериал с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        /// <returns>Экземпляр сериала</returns>
        TVSeries GetTvSeries(int id);

        /// <summary>
        /// Возвращает страницу сериалов с заданным номером и размером,
        /// сериалы сортированы по рейтингу
        /// </summary>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <returns>Страница сериалов</returns>
        TvSeriesPageInfo GetPageInfoByRating(int pageSize, int pageNumber);

        /// <summary>
        /// Добавляет заданный сериал в хранилище данных
        /// и возвращает идентификатор добавленного сериала.
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для добавления</param>
        /// <returns>Идентификатор сериала</returns>
        int AddTvSeries(TVSeries tvSeries);

        /// <summary>
        /// Обновляет информацию заданного сериала в хранилище данных
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для обновления</param>
        void UpdateTvSeries(TVSeries tvSeries);

        /// <summary>
        /// Удаляет сериал с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        void DeleteTvSeries(int id);

        /// <summary>
        /// Проверяет наличие сериала в хранилище данных
        /// соответствующего заданному фильтру
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска сериала</param>
        /// <returns>Возвращает <see langword="true"/>, если сериал существует в хранилище данных</returns>
        bool TvSeriesExists(Expression<Func<TVSeries, bool>> filter);
    }
}