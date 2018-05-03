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
    /// о фильмах в хранилище данных
    /// </summary>
    public class FilmDetailedInfoService : IFilmDetailedInfoService
    {
        private IDbFilmUnitOfWork _unitOfWork;
        private bool disposedValue = false;

        /// <summary>
        /// Создает новый экземпляр <see cref="FilmDetailedInfoService"/>
        /// с заданным unit of work
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        public FilmDetailedInfoService(IDbFilmUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Возвращает фильма с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns>Экземпляр фильма</returns>
        public Film GetFilm(int id)
        {
            return Mapper.Map<Film>(
                this._unitOfWork.FilmRepository.GetById(id));
        }

        /// <summary>
        /// Добавляет заданный фильм в хранилище данных
        /// и возвращает идентификатор добавленного фильма.
        /// </summary>
        /// <param name="film">Экземпляр фильма для добавления</param>
        /// <returns>Идентификатор фильма</returns>
        public int AddFilm(Film film)
        {
            var dbFilm = Mapper.Map<DbFilm>(film);
            this._unitOfWork.FilmRepository.Insert(dbFilm);
            this._unitOfWork.Save();
            return dbFilm.Id;
        }

        /// <summary>
        /// Обновляет информацию заданного фильма в хранилище данных
        /// </summary>
        /// <param name="film">Экземпляр фильма для обновления</param>
        public void UpdateFilm(Film film)
        {
            var dbFilm = Mapper.Map<DbFilm>(film);
            this._unitOfWork.FilmRepository.Update(dbFilm);
            this._unitOfWork.Save();
        }

        /// <summary>
        /// Удаляет фильм с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        public void DeleteFilm(int id)
        {
            this._unitOfWork.FilmRepository.Delete(id);
            this._unitOfWork.Save();
        }

        /// <summary>
        /// Проверяет наличие фильма в хранилище данных
        /// соответствующего заданному фильтру
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска фильма</param>
        /// <returns>Возвращает <see langword="true"/>, если фильм существует в хранилище данных</returns>
        public bool FilmExists(Expression<Func<Film, bool>> filter)
        {
            return this._unitOfWork.FilmRepository.Get(
                Mapper.Map<Expression<Func<DbFilm, bool>>>(filter))
                .FirstOrDefault() != null;
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        /// <param name="disposing">Указывает был ли уже вызван метода Dispose ранее</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._unitOfWork.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
