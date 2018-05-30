using Rocket.Web.Hubs;

namespace Rocket.Web.Helpers
{
    /// <summary>
    /// Выполняет рассылку push-уведомлений
    /// </summary>
    public class PushNotificationsHelper
    {
        /// <summary>
        /// Отослать сообщение всем активным пользователям
        /// </summary>
        /// <param name="msg">Сообщение</param>
        public static void SendPushNotificationsToAll(object msg)
        {
            NotificationHub.NotifyAll(msg);
        }
    }
}