﻿using System.Data.Entity;
using Rocket.DAL.Common.DbModels.Notification;
using Rocket.DAL.Common.Repositories.Notification;

namespace Rocket.DAL.Repositories.Notification
{
    /// <summary>
    /// Представляет репозиторий для сообщений с информацией о совершенном гостем донате
    /// </summary>
    public class DbGuestBillingMessageRepository : BaseRepository<DbGuestBillingMessage>, IDbGuestBillingMessageRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для сообщений с информацией о совершенном гостем донате
        /// с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbGuestBillingMessageRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
