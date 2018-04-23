using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Представляет сервис для работы с детальной информацией
    /// о сериалах в хранилище данных
    /// </summary>
    interface ITVSeriesDetailedInfoService
    {
        /// <summary>
        /// Возвращает сериал с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        /// <returns>Экземпляр сериала</returns>
        TVSeries GetTVSeries(int id);

        /// <summary>
        /// Добавляет заданный сериал в хранилище данных
        /// и возвращает идентификатор добавленного сериала.
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для добавления</param>
        /// <returns>Идентификатор сериала</returns>
        int AddTVSeries(TVSeries tvSeries);

        /// <summary>
        /// Обновляет информацию заданного сериала в хранилище данных
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для обновления</param>
        void UpdateTVSeries(TVSeries tvSeries);

        /// <summary>
        /// Удаляет сериал с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        void DeleteTVSeries(int id);

        /// <summary>
        /// Проверяет наличие заданного сериала в хранилище данных
        /// </summary>
        /// <param name="tvSeries">Экземпляр сериала для проверки</param>
        /// <returns>Возвращает <see langword="true"/>, если сериал существует в хранилище данных</returns>
        bool TVSeriesExists(TVSeries tvSeries);
    }
}
