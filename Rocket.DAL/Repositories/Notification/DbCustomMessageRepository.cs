using System.Data.Entity;
using Rocket.DAL.Common.DbModels.Notification;
using Rocket.DAL.Common.Repositories.Notification;

namespace Rocket.DAL.Repositories.Notification
{
    /// <summary>
    /// Представляет репозиторий для сообщений произвольного содержания
    /// </summary>
    public class DbCustomMessageRepository : BaseRepository<DbCustomMessage>, IDbCustomMessageRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для сообщений произвольного содержания
        /// с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbCustomMessageRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}