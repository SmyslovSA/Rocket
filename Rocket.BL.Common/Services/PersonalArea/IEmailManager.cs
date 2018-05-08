namespace Rocket.BL.Common.Services.PersonalArea
{
    /// <summary>
    /// интерфейс для работы с email-адресами
    /// </summary>
    public interface IEmailManager
    {
        /// <summary>
        /// добавление нового e-mail для отправки нотификаций
        /// </summary>
        /// <param name="userId">id пользователя, инициировавшего добавление нового e-mail</param>
        /// <param name="email">адрес e-mail, который необходимо добавить</param>
        /// <returns>
        /// true - если e-mail успешно добавлен
        /// </returns>
        bool AddEmail(int userId, string email);
        /// <summary>
        /// удаление из приложения одного из имеющихся e-mail пользователя 
        /// </summary>
        /// <param name="userId">id пользователя, инициировавшего удаление нового e-mail</param>
        /// <param name="email">адрес e-mail, который необходимо удалить</param>
        /// <returns>
        /// true - если e-mail успешно удален
        /// </returns>
        bool DeleteEmail(int userId, string email);
    }
}
