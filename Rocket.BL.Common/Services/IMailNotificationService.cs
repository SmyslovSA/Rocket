using Rocket.BL.Common.Models.Notification;

namespace Rocket.BL.Common.Services
{
    /// <summary>
    /// Интерфейс взаимодействия с сервисом email нотификации
    /// </summary>
    public interface IMailNotificationService
    {
        /// <summary>
        /// Отправка пользователю сообщения с информацией об оплате премиум аккаунта
        /// </summary>
        /// <param name="message">Содержит данные о совершенной оплате</param>
        void SendBilling(BillingMessage message);

        /// <summary>
        /// Отправка гостю либо пользователю сообщения с благодарностью за совершенный донат
        /// </summary>
        /// <param name="message">Содержит данные о совершенной оплате</param>
        void SendGratitude(BillingMessage message);

        /// <summary>
        /// Отправка сообщения произвольного содержания
        /// </summary>
        /// <param name="message">Описывает сообщение произвольного содержания</param>
        void SendCustomMessage(CustomMessage message);

        /// <summary>
        /// Отправка сообщения о релизе фильма
        /// </summary>
        /// <param name="film">Описывает сообщение о релизе фильма</param>
        void NotifyAboutUser(MessageAboutUser film);

        /// <summary>
        /// Отправка сообщения о музыкальном релизе
        /// </summary>
        /// <param name="music">Описывает сообщение о музыкальном релизе</param>
        void NotifyAboutMusic(MessageAboutMusic music);

        /// <summary>
        /// Отправка сообщения о релизе сериала
        /// </summary>
        /// <param name="tvSeries">Описывает сообщение о релизе сериала</param>
        void NotifyAboutTVSeries(MessageAboutTVSeries tvSeries);
    }
}