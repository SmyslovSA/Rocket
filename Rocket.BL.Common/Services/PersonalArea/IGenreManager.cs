using Rocket.BL.Common.Models.PersonalArea;

namespace Rocket.BL.Common.Services.PersonalArea
{
    /// <summary>
    /// Интерфейс для работы с жанрами.
    /// </summary>
    public interface IGenreManager
    {
        /// <summary>
        /// Добавление жанра определенной категории в персональный список ожидания релизов.
        /// </summary>
        /// <param name="idUser">id user идентификатор польз..</param>
        /// <param name="category">Категория продукта, в которой пользователь хочет добавить новый жанр.</param>
        /// <param name="genre">Жанр продукта, который пользователь хочет добавить в список.</param>
        /// <returns>
        /// True - при успешном добавлении жанра в список пользователя.
        /// </returns>
        bool AddGenre(int idUser, string category, string genre);

        /// <summary>
        /// Удаление жанра определенной категории из персонального списка ожидания релизов.
        /// </summary>
        /// <param name="idUser">id user идентифик.</param>
        /// <param name="category">Категория продукта, в которой пользователь хочет удалить новый жанр.</param>
        /// <param name="genre">Жанр продукта, который пользователь хочет удалить из списка.</param>
        /// <returns>
        /// True - при успешном удалении жанра из списка пользователя.
        /// </returns>
        bool DeleteGenre(int idUser, string category, string genre);
    }
}