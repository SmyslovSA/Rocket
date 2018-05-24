using System;
using Rocket.DAL.Common.Enums;

namespace Rocket.DAL.Common.DbModels.Notification
{
    /// <summary>
    /// Лог нотификации
    /// </summary>
    public class NotificationsLogEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип уведомления
        /// </summary>
        public NotificationType NotificationType { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Тип релиза
        /// </summary>
        public ReleaseType ReleaseType { get; set; }

        /// <summary>
        /// Id релиза
        /// </summary>
        public int ReleaseId { get; set; }

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime CreatedDateTime { get; private set; }
    }
}
