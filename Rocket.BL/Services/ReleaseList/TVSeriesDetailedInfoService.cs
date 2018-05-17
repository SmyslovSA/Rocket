using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Services.ReleaseList;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.ReleaseList
{
    /// <summary>
    /// Представляет сервис для работы с детальной информацией
    /// о сериалах в хранилище данных
    /// </summary>
    public class TVSeriesDetailedInfoService : BaseService, ITVSeriesDetailedInfoService
    {
        ///// <summary>
        ///// Создает новый экземпляр <see cref="TVSeriesDetailedInfoService"/>
        ///// с заданным unit of work
        ///// </summary>
        ///// <param name="unitOfWork">Экземпляр unit of work</param>
        //public TVSeriesDetailedInfoService(IUnitOfWork unitOfWork)
        //    : base(unitOfWork)
        //{
        //}

        ///// <summary>
        ///// Возвращает сериал с заданным идентификатором из хранилища данных
        ///// </summary>
        ///// <param name="id">Идентификатор сериала</param>
        ///// <returns>Экземпляр сериала</returns>
        //public TVSeries GetTVSeries(int id)
        //{
        //    return Mapper.Map<TVSeries>(
        //        this._unitOfWork.TVSeriesRepository.GetById(id));
        //}

        ///// <summary>
        ///// Добавляет заданный сериал в хранилище данных
        ///// и возвращает идентификатор добавленного сериала.
        ///// </summary>
        ///// <param name="film">Экземпляр сериала для добавления</param>
        ///// <returns>Идентификатор сериала</returns>
        //public int AddTVSeries(TVSeries tvSeries)
        //{
        //    var dbTVSeries = Mapper.Map<DbTVSeries>(tvSeries);
        //    this._unitOfWork.TVSeriesRepository.Insert(dbTVSeries);
        //    this._unitOfWork.SaveChanges();
        //    return dbTVSeries.Id;
        //}

        ///// <summary>
        ///// Обновляет информацию заданного сериала в хранилище данных
        ///// </summary>
        ///// <param name="film">Экземпляр сериала для обновления</param>
        //public void UpdateTVSeries(TVSeries tvSeries)
        //{
        //    var dbTVSeries = Mapper.Map<DbTVSeries>(tvSeries);
        //    this._unitOfWork.TVSeriesRepository.Update(dbTVSeries);
        //    this._unitOfWork.SaveChanges();
        //}

        ///// <summary>
        ///// Удаляет сериал с заданным идентификатором из хранилища данных.
        ///// </summary>
        ///// <param name="id">Идентификатор сериала</param>
        //public void DeleteTVSeries(int id)
        //{
        //    this._unitOfWork.TVSeriesRepository.Delete(id);
        //    this._unitOfWork.SaveChanges();
        //}

        ///// <summary>
        ///// Проверяет наличие сериала в хранилище данных
        ///// соответствующего заданному фильтру
        ///// </summary>
        ///// <param name="filter">Лямбда-выражение определяющее фильтр для поиска сериала</param>
        ///// <returns>Возвращает <see langword="true"/>, если сериал существует в хранилище данных</returns>
        //public bool TVSeriesExists(Expression<Func<TVSeries, bool>> filter)
        //{
        //    return this._unitOfWork.TVSeriesRepository.Get(
        //        Mapper.Map<Expression<Func<DbTVSeries, bool>>>(filter))
        //        .FirstOrDefault() != null;
        //}
        
        public TVSeriesDetailedInfoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public TVSeries GetTVSeries(int id)
        {
            return Mapper.Map<TVSeries>(
                _unitOfWork.TvSeriasRepository.GetById(id));
        }

        /// <summary>
        /// Добавляет заданный сериал в хранилище данных
        /// и возвращает идентификатор добавленного сериала.
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для добавления</param>
        /// <returns>Идентификатор сериала</returns>
        public int AddTVSeries(TVSeries tvSeries)
        {
            var dbTVSeries = Mapper.Map<TvSeriasEntity>(tvSeries);
            _unitOfWork.TvSeriasRepository.Insert(dbTVSeries);
            _unitOfWork.SaveChanges();
            return dbTVSeries.Id;
        }

        /// <summary>
        /// Обновляет информацию заданного сериала в хранилище данных
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для обновления</param>
        public void UpdateTVSeries(TVSeries tvSeries)
        {
            var dbTVSeries = Mapper.Map<TvSeriasEntity>(tvSeries);
            _unitOfWork.TvSeriasRepository.Update(dbTVSeries);
            _unitOfWork.SaveChanges();
        }

        public void DeleteTVSeries(int id)
        {
            _unitOfWork.TvSeriasRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public bool TVSeriesExists(Expression<Func<TVSeries, bool>> filter)
        {
            return _unitOfWork.TvSeriasRepository.Get(
                           Mapper.Map<Expression<Func<TvSeriasEntity, bool>>>(filter))
                       .FirstOrDefault() != null;
        }
    }
}