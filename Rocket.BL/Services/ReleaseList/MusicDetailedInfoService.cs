using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Services.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.UoW;
using System;
using System.Linq;
using System.Linq.Expressions;

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
			return Mapper.Map<Music>(
				this._unitOfWork.MusicRepository.GetById(id));
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
			this._unitOfWork.MusicRepository.Insert(dbMusic);
			this._unitOfWork.Save();
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
			this._unitOfWork.MusicRepository.Update(dbMusic);
			this._unitOfWork.Save();
		}

		/// <inheritdoc />
		/// <summary>
		/// Удаляет музыкальный релиз с заданным идентификатором из хранилища данных.
		/// </summary>
		/// <param name="id">Идентификатор музыкального релиза</param>
		public void DeleteMusic(int id)
		{
			this._unitOfWork.MusicRepository.Delete(id);
			this._unitOfWork.Save();
		}

		/// <summary>
		/// Проверяет наличие музыкального релиза в хранилище данных
		/// соответствующего заданному фильтру
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public bool MusicExists(Expression<Func<Music, bool>> filter)
		{
			return this._unitOfWork.MusicRepository.Get(
					       Mapper.Map<Expression<Func<DbMusic, bool>>>(filter))
				       .FirstOrDefault() != null;
		}
	}
}
