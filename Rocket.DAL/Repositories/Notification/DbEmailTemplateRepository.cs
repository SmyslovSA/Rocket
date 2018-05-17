using Rocket.DAL.Common.DbModels.Notification;
using Rocket.DAL.Common.Repositories.Notification;

namespace Rocket.DAL.Repositories.Notification
{
    /// <summary>
    /// Представляет репозиторий для сообщений произвольного содержания
    /// </summary>
    public class DbEmailTemplateRepository: BaseRepository<DbEmailTemplate>, IDbEmailTemplateRepository
    {

    }
}
