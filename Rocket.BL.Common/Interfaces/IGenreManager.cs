using Rocket.BL.Common.Enums;

namespace Rocket.BL.Common.Models
{
    /// <summary>
    /// Интерфейс для добавления/удаления жанра определенной категории в персональную ленту
    /// </summary>
    interface IGenreManager
    {
        bool AddGenre(User user, Categories category, Genres genre);
        bool DeleteGenre(User user, Categories category, Genres genre);
    }
}
