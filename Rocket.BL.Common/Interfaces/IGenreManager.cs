namespace Rocket.BL.Common.Models
{
    interface IGenreManager
    {
        /// <summary>
        /// добавления жанра определенной категории в персональный список ожидания релизов
        /// </summary>
        /// <param name="user">пользователь, инициировавший добавление нового жанра</param>
        /// <param name="category">категория продукта, в которой пользователь хочет добавить новый жанр</param>
        /// <param name="genre">жанр продукта, который пользователь хочет добавить в список</param>
        /// <returns>
        /// true - при успешном добавлении жанра в список пользователя
        /// </returns>
        bool AddGenre(AuthorisedUser user, string category, string genre);
        /// <summary>
        /// удаление жанра определенной категории из персонального списка ожидания релизов
        /// </summary>
        /// <param name="user">пользователь, инициировавший добавление нового жанра</param>
        /// <param name="category">категория продукта, в которой пользователь хочет удалить новый жанр</param>
        /// <param name="genre">жанр продукта, который пользователь хочет удалить из списка</param>
        /// <returns>
        /// true - при успешном удалении жанра из списка пользователя
        /// </returns>
        bool DeleteGenre(AuthorisedUser user, string category, string genre);
    }
}
