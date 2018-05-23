using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Services.ReleaseList;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rocket.BL.Common.Models.Pagination;

namespace Rocket.BL.Services.ReleaseList
{
    /// <summary>
    /// Представляет сервис для работы с детальной информацией
    /// о сериалах в хранилище данных
    /// </summary>
    public class TvSeriesDetailedInfoService : BaseService, ITvSeriesDetailedInfoService
    {
        /// <summary>
        /// Создает новый экземпляр <see cref="TvSeriesDetailedInfoService"/>
        /// с заданным unit of work
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        public TvSeriesDetailedInfoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Возвращает сериал с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        /// <returns>Экземпляр сериала</returns>
        public TVSeries GetTvSeries(int id)
        {
            var tvSeries = Mapper.Map<TVSeries>(
                _unitOfWork.TvSeriasRepository.Get(
                    f => f.Id == id,
                    includeProperties: $"{nameof(TvSeriasEntity.ListGenreEntity)},{nameof(TvSeriasEntity.ListSeasons)},{nameof(TvSeriasEntity.ListPerson)}")
                    ?.FirstOrDefault());
            
            if (tvSeries?.ListSeasons == null)
            {
                return tvSeries;
            }

            foreach (var season in tvSeries.ListSeasons)
            {
                season.ListEpisode = Mapper.Map<ICollection<Episode>>(
                    _unitOfWork.EpisodeRepository.Get(
                        e => e.SeasonId == season.Id));
            }

            return tvSeries;
        }

        /// <summary>
        /// Возвращает страницу сериалов с заданным номером и размером,
        /// сериалы сортированы по рейтингу
        /// </summary>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <returns>Страница сериалов</returns>
        public TvSeriesPageInfo GetPageInfoByRating(int pageSize, int pageNumber)
        {
            var pageInfo = new TvSeriesPageInfo();
            pageInfo.TotalItemsCount = _unitOfWork.TvSeriasRepository.ItemsCount();
            pageInfo.TotalPagesCount = (int)Math.Ceiling((double)pageInfo.TotalItemsCount / pageSize);
            pageInfo.PageItems = Mapper.Map<IEnumerable<TVSeries>>(
                _unitOfWork.TvSeriasRepository.GetPage(pageSize, pageNumber, orderBy: o => o.OrderByDescending(t => t.RateImDb)));

            return pageInfo;
        }

        /// <summary>
        /// Добавляет заданный сериал в хранилище данных
        /// и возвращает идентификатор добавленного сериала.
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для добавления</param>
        /// <returns>Идентификатор сериала</returns>
        public int AddTvSeries(TVSeries tvSeries)
        {
            var dbTvSeries = Mapper.Map<TvSeriasEntity>(tvSeries);
            _unitOfWork.TvSeriasRepository.Insert(dbTvSeries);
            _unitOfWork.SaveChanges();
            return dbTvSeries.Id;
        }

        /// <summary>
        /// Обновляет информацию заданного сериала в хранилище данных
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для обновления</param>
        public void UpdateTvSeries(TVSeries tvSeries)
        {
            var dbTvSeries = Mapper.Map<TvSeriasEntity>(tvSeries);
            _unitOfWork.TvSeriasRepository.Update(dbTvSeries);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Удаляет сериал с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        public void DeleteTvSeries(int id)
        {
            _unitOfWork.TvSeriasRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Проверяет наличие сериала в хранилище данных
        /// соответствующего заданному фильтру
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска сериала</param>
        /// <returns>Возвращает <see langword="true"/>, если сериал существует в хранилище данных</returns>
        public bool TvSeriesExists(Expression<Func<TVSeries, bool>> filter)
        {
            return _unitOfWork.TvSeriasRepository.Get(
                Mapper.Map<Expression<Func<TvSeriasEntity, bool>>>(filter))
                .FirstOrDefault() != null;
        }
    }
}