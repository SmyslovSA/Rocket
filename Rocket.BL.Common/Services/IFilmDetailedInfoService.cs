using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Представляет сервис для работы с детальной информацией
    /// о фильмах в хранилище данных
    /// </summary>
    public interface IFilmDetailedInfoService
    {
        /// <summary>
        /// Возвращает фильма с заданным идентификатором из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns>Экземпляр фильма</returns>
        Film GetFilm(int id);

        /// <summary>
        /// Добавляет заданный фильм в хранилище данных
        /// и возвращает идентификатор добавленного фильма.
        /// Если добавление фильма завершилось неудачей,
        /// возвращает -1.
        /// </summary>
        /// <param name="film">Экземпляр фильма для добавления</param>
        /// <returns>Идентификатор фильма</returns>
        int AddFilm(Film film);

        /// <summary>
        /// Обновляет информацию заданного фильма в хранилище данных
        /// и возвращает значение, которое определяет
        /// прошла ли операция успешно
        /// </summary>
        /// <param name="film">Экземпляр фильма для обновления</param>
        /// <returns>Возвращает <see langword="true"/> при успешном обновлении</returns>
        bool UpdateFilm(Film film);

        /// <summary>
        /// Удаляет фильм с заданным идентификатором из хранилища данных.
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns>Возвращает <see langword="true"/> при успешном удалении</returns>
        bool DeleteFilm(int id);

        /// <summary>
        /// Проверяет наличие заданного фильма в хранилище данных
        /// </summary>
        /// <param name="film">Экземпляр фильма для проверки</param>
        /// <returns>Возвращает <see langword="true"/>, если фильм существует в хранилище данных</returns>
        bool FilmExists(Film film);
    }
}
