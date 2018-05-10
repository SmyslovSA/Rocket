using Rocket.BL.Common.Models.PersonalArea;

namespace Rocket.BL.Common.Services.PersonalArea
{
    /// <summary>
    /// интерфейс для работы с жанрами
    /// </summary>
    public interface IGenreManager
    {
        /// <summary>
        /// добавления жанра определенной категории в персональный список ожидания релизов
        /// </summary>
        /// <param name="userId">id пользователя, инициировавшего добавление нового жанра</param>
        /// <param name="category">категория продукта, в которой пользователь хочет добавить новый жанр</param>
        /// <param name="genre">жанр продукта, который пользователь хочет добавить в список</param>
        /// <returns>
        /// true - при успешном добавлении жанра в список пользователя
        /// </returns>
        bool AddGenre(SimpleUser model, string category, string genre);
        /// <summary>
        /// удаление жанра определенной категории из персонального списка ожидания релизов
        /// </summary>
        /// <param name="userId">id пользователя, инициировавшего добавление нового жанра</param>
        /// <param name="category">категория продукта, в которой пользователь хочет удалить новый жанр</param>
        /// <param name="genre">жанр продукта, который пользователь хочет удалить из списка</param>
        /// <returns>
        /// true - при успешном удалении жанра из списка пользователя
        /// </returns>
        bool DeleteGenre(SimpleUser model, string category, string genre);
    }
}
