using Rocket.BL.Common.Models.PersonalArea;

namespace Rocket.BL.Common.Services.PersonalArea
{
    /// <summary>
    /// Интерфейс для работы с email-адресами.
    /// </summary>
    public interface IEmailManager
    {
        /// <summary>
        /// Добавление нового e-mail для отправки нотификаций.
        /// </summary>
        /// <param name="model">Модель пользователя, инициировавшего добавление нового e-mail.</param>
        /// <param name="email">Адрес e-mail, который необходимо добавить.</param>
        /// <returns>
        /// True - если e-mail успешно добавлен.
        /// </returns>
        bool AddEmail(SimpleUser model, string email);

        /// <summary>
        /// Удаление из приложения одного из имеющихся e-mail пользователя.
        /// </summary>
        /// <param name="model">Модель пользователя, инициировавшего удаление нового e-mail.</param>
        /// <param name="email">Адрес e-mail, который необходимо удалить.</param>
        /// <returns>
        /// True - если e-mail успешно удален.
        /// </returns>
        bool DeleteEmail(SimpleUser model, string email);
    }
}