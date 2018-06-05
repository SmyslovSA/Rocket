using Rocket.BL.Common.DtoModels.ReleaseList;
using Rocket.BL.Common.Models.Pagination;
using System;

namespace Rocket.BL.Common.Services.ReleaseList
{
    public interface IEpisodeService : IDisposable
    {
        /// <summary>
        /// Возвращает серию с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор серии</param>
        /// <returns>Серия</returns>
        EpisodeFullDto GetEpisodesById(int id);

        /// <summary>
        /// Возвращает страницу новых серий с заданным номером и размером,
        /// серии сортированы по дате выхода
        /// </summary>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="genreId">Идентификатор жанра</param>
        /// <returns>Страница серий</returns>
        PageInfo<EpisodeFullDto> GetNewEpisodesPage(int pageSize, int pageNumber, int? genreId = null);

        /// <summary>
        /// Возвращает страницу будущих серий с заданным номером и размером,
        /// серии сортированы по дате выхода
        /// </summary>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <returns>Страница серий</returns>
        PageInfo<EpisodeDto> GetScheduleEpisodesPage(int pageSize, int pageNumber);
    }
}
