using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Context;

namespace Rocket.DAL.Repositories.PersonalArea
{
    /// <summary>
    /// Репозиторий жанров.
    /// </summary>
    public class DbGenreRepository : BaseRepository<DbGenre>, IDbGenreRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для жанров с заданным контекстом базы данных.
        /// </summary>
        /// <param name="context">Экземпляр контекста базы данных.</param>
        public DbGenreRepository(RocketContext context) : base(context)
        {
        }
    }
}