using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Services;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.UoW;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.BL.Services
{
    /// <summary>
    /// Представляет сервис для работы с детальной информацией
    /// о сериалах в хранилище данных
    /// </summary>
    public class TVSeriesDetailedInfoService : DisposableService, ITVSeriesDetailedInfoService
    {
        /// <summary>
        /// Создает новый экземпляр <see cref="TVSeriesDetailedInfoService"/>
        /// с заданным unit of work
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        public TVSeriesDetailedInfoService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Возвращает сериал с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        /// <returns>Экземпляр сериала</returns>
        public TVSeries GetTVSeries(int id)
        {
            return Mapper.Map<TVSeries>(
                this._unitOfWork.TVSeriesRepository.GetById(id));
        }

        /// <summary>
        /// Добавляет заданный сериал в хранилище данных
        /// и возвращает идентификатор добавленного сериала.
        /// </summary>
        /// <param name="film">Экземпляр сериала для добавления</param>
        /// <returns>Идентификатор сериала</returns>
        public int AddTVSeries(TVSeries tvSeries)
        {
            var dbTVSeries = Mapper.Map<DbTVSeries>(tvSeries);
            this._unitOfWork.TVSeriesRepository.Insert(dbTVSeries);
            this._unitOfWork.Save();
            return dbTVSeries.Id;
        }

        /// <summary>
        /// Обновляет информацию заданного сериала в хранилище данных
        /// </summary>
        /// <param name="film">Экземпляр сериала для обновления</param>
        public void UpdateTVSeries(TVSeries tvSeries)
        {
            var dbTVSeries = Mapper.Map<DbTVSeries>(tvSeries);
            this._unitOfWork.TVSeriesRepository.Update(dbTVSeries);
            this._unitOfWork.Save();
        }

        /// <summary>
        /// Удаляет сериал с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        public void DeleteTVSeries(int id)
        {
            this._unitOfWork.TVSeriesRepository.Delete(id);
            this._unitOfWork.Save();
        }

        /// <summary>
        /// Проверяет наличие сериала в хранилище данных
        /// соответствующего заданному фильтру
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска сериала</param>
        /// <returns>Возвращает <see langword="true"/>, если сериал существует в хранилище данных</returns>
        public bool TVSeriesExists(Expression<Func<TVSeries, bool>> filter)
        {
            return this._unitOfWork.TVSeriesRepository.Get(
                Mapper.Map<Expression<Func<DbTVSeries, bool>>>(filter))
                .FirstOrDefault() != null;
        }
    }
}
