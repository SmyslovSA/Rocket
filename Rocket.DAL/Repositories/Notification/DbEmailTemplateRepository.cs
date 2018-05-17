using System.Data.Entity;
using Rocket.DAL.Common.DbModels.Notification;
using Rocket.DAL.Common.Repositories.Notification;

namespace Rocket.DAL.Repositories.Notification
{
    /// <summary>
    /// Представляет репозиторий для шаблонов email сообщений
    /// </summary>
    public class DbEmailTemplateRepository: BaseRepository<DbEmailTemplate>, IDbEmailTemplateRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для шаблонов email сообщений
        /// с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbEmailTemplateRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
