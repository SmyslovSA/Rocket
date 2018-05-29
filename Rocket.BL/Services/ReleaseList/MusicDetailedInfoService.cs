using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Services.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;
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
    /// о музыкальных релизах в хранилище данных
    /// </summary>
    public class MusicDetailedInfoService : BaseService, IMusicDetailedInfoService
    {
        /// <summary>
        /// Создает новый экземпляр <see cref="MusicDetailedInfoService"/>
        /// с заданным unit of work
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        public MusicDetailedInfoService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Возвращает музыкальный релиз с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор музыкального релиза</param>
        /// <returns>Экземпляр музыкального релиза</returns>
        public Music GetMusic(int id)
        {
            //return Mapper.Map<Music>(
            //_unitOfWork.MusicRepository.GetById(id));

            var model = Mapper.Map<Music>(
                _unitOfWork.MusicRepository.Get(
                        f => f.Id == id,
                        includeProperties: $"{nameof(Music.Genres)},{nameof(Music.MusicTracks)},{nameof(Music.Musicians)}")
                    ?.FirstOrDefault());

            return model;
        }

        /// <inheritdoc />
        /// <summary>
        /// Добавляет заданный музыкальный релиз в хранилище данных
        /// и возвращает идентификатор добавленного музыкального релиза.
        /// </summary>
        /// <param name="music">Экземпляр музыкального релиза для добавления</param>
        /// <returns>Идентификатор музыкального релиза</returns>
        public int AddMusic(Music music)
        {
            var dbMusic = Mapper.Map<DbMusic>(music);
            _unitOfWork.MusicRepository.Insert(dbMusic);
            _unitOfWork.SaveChanges();
            return dbMusic.Id;
        }

        /// <inheritdoc />
        /// <summary>
        /// Обновляет информацию заданного музыкального релиза в хранилище данных
        /// </summary>
        /// <param name="music">Экземпляр музыкального релиза для обновления</param>
        public void UpdateMusic(Music music)
        {
            var dbMusic = Mapper.Map<DbMusic>(music);
            _unitOfWork.MusicRepository.Update(dbMusic);
            _unitOfWork.SaveChanges();
        }

        /// <inheritdoc />
        /// <summary>
        /// Удаляет музыкальный релиз с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор музыкального релиза</param>
        public void DeleteMusic(int id)
        {
            _unitOfWork.MusicRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Проверяет наличие музыкального релиза в хранилище данных
        /// соответствующего заданному фильтру
        /// </summary>
        /// <param name="filter">фильтр</param>
        /// <returns>bool</returns>
        public bool MusicExists(Expression<Func<Music, bool>> filter)
        {
            return _unitOfWork.MusicRepository.Get(
                           Mapper.Map<Expression<Func<DbMusic, bool>>>(filter))
                       .FirstOrDefault() != null;
        }

		/// <summary>
		/// Возвращает страницу музыкальных релизов с заданным номером и размером,
		/// музыкальные релизы сортированы по дата релиза
		/// </summary>
		/// <param name="pageSize">Размер страницы</param>
		/// <param name="pageNumber">Номер страницы</param>
		/// <param name="genreId">Идентификатор жанра</param>
		/// <returns>Страница музыкальных релизов</returns>
		public MusicPageInfo GetPageInfoByDate(int pageSize, int pageNumber, int? genreId = null)
        {
	        Expression<Func<DbMusic, bool>> filter = null;
	        if (genreId != null)
	        {
		        filter = f => f.Genres.Select(g => g.Id).Contains(genreId.Value);
	        }

			var pageInfo = new MusicPageInfo();
            pageInfo.TotalItemsCount = _unitOfWork.MusicRepository.ItemsCount(filter);
            pageInfo.TotalPagesCount = (int)Math.Ceiling((double)pageInfo.TotalItemsCount / pageSize);
            pageInfo.PageItems = Mapper.Map<IEnumerable<Music>>(_unitOfWork.MusicRepository.GetPage(
                pageSize,
                pageNumber,
                filter,
				o => o.OrderByDescending(t => t.ReleaseDate)));

            return pageInfo;
        }
    }
}