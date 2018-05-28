using System.Threading.Tasks;

namespace Rocket.Notifications.Interfaces
{
    /// <summary>
    /// Рассылка згыр
    /// </summary>
    public interface IPushNotifications
    {
        /// <summary>
        /// Отправка уведомлений
        /// </summary>
        /// <returns></returns>
        Task NotifyAsync();
    }
}