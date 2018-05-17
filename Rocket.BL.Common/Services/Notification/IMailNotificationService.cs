using Rocket.BL.Common.Models.Notification;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Интерфейс взаимодействия с сервисом email нотификации
    /// </summary>
    public interface IMailNotificationService
    {
        /// <summary>
        /// Отправка посетителю сообщения со ссылкой, необходимой
        /// для завершения регистрации аккаунта
        /// </summary>
        /// <param name="email">Email адрес посетителя</param>
        /// <param name="url">Ссылка для завершения регистрации аккаунта</param>
        /// <param name="name">Имя посетителя</param>
        void SendConfirmation(string email, string url, string name = "посетитель");

        /// <summary>
        /// Отправка пользователю сообщения с информацией об оплате премиум аккаунта
        /// </summary>
        /// <param name="id">Идентификационный номер сообщения <see cref="DbUserBillingMessage"/></param>
        void SendBillingPremium(int id);

        /// <summary>
        /// Отправка гостю сообщения с благодарностью за совершенный донат
        /// </summary>
        /// <param name="id">Идентификационный номер сообщения <see cref="DbGuestBillingMessage"/></param>
        void SendBillingUser(int id);

        /// <summary>
        /// Отправка сообщения произвольного содержания
        /// </summary>
        /// <param name="id">Идентификационный номер сообщения <see cref="DbCustomMessage"/></param>
        void SendCustomMessage(int id);

        /// <summary>
        /// Отправка сообщения о релизе
        /// </summary>
        /// <param name="id">Идентификационный номер сообщения <see cref="DbReleaseMessage"/></param>
        void NotifyAboutRelease(int id);
    }
}