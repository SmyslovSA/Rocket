namespace Rocket.BL.Common.Models
{
    interface IEmailManager
    {
        /// <summary>
        /// добавление нового e-mail для отправки нотификаций
        /// </summary>
        /// <param name="user">пользователь, инициировавший добавление нового e-mail</param>
        /// <param name="email">адрес e-mail, который необходимо добавить</param>
        /// <returns>
        /// true - если e-mail успешно добавлен
        /// </returns>
        bool AddEmail(AuthorisedUser user, string email);
        /// <summary>
        /// удаление из приложения одного из имеющихся e-mail пользователя 
        /// </summary>
        /// <param name="user">пользователь, инициировавший удаление нового e-mail</param>
        /// <param name="email">адрес e-mail, который необходимо удалить</param>
        /// <returns>
        /// true - если e-mail успешно удален
        /// </returns>
        bool DeleteEmail(AuthorisedUser user, string email);
    }
}
