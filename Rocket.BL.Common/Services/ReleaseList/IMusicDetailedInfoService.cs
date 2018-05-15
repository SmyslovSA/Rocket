using System;
using System.Linq.Expressions;
using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Common.Services.ReleaseList
{
    /// <summary>
    /// Представляет сервис для работы с детальной информацией
    /// о музыкальных релизах в хранилище данных
    /// </summary>
    public interface IMusicDetailedInfoService : IDisposable
    {
        /// <summary>
        /// Возвращает музыкальный релиз с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор музыкального релиза</param>
        /// <returns>Экземпляр музыкального релиза</returns>
        Music GetMusic(int id);

        /// <summary>
        /// Добавляет заданный музыкальный релиз в хранилище данных
        /// и возвращает идентификатор добавленного музыкального релиза.
        /// </summary>
        /// <param name="music">Экземпляр музыкального релиза для добавления</param>
        /// <returns>Идентификатор музыкального релиза</returns>
        int AddMusic(Music music);

        /// <summary>
        /// Обновляет информацию заданного музыкального релиза в хранилище данных
        /// </summary>
        /// <param name="music">Экземпляр музыкального релиза для обновления</param>
        void UpdateMusic(Music music);

        /// <summary>
        /// Удаляет музыкальный релиз с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор музыкального релиза</param>
        void DeleteMusic(int id);

        /// <summary>
        /// Проверяет наличие заданного музыкального релиза в хранилище данных
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтр для поиска музыки</param>
        /// <returns>Возвращает <see langword="true"/>, если музыкальный релиз существует в хранилище данных</returns>
        bool MusicExists(Expression<Func<Music, bool>> filter);
    }
}