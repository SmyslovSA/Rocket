using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Common.Repositories.Notification
{
    /// <summary>
    /// Представляет интерфейс репозитория для пользователей, являющихся получателями сообщений
    /// </summary>
    interface IDbReceiverRepository : IBaseRepository<DbReceiver>
    {
    }
}