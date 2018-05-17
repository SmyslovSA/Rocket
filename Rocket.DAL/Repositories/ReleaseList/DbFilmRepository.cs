using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Context;

namespace Rocket.DAL.Repositories.ReleaseList
{
    /// <summary>
    /// Представляет репозиторий для фильмов
    /// </summary>
    public class DbFilmRepository : BaseRepository<DbFilm>, IDbFilmRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для фильмов с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbFilmRepository(RocketContext dbContext)
            : base(dbContext)
        {
        }
    }
}