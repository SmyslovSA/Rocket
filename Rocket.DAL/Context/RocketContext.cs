using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Configurations.ReleaseList;
using System.Data.Entity;

namespace Rocket.DAL.Context
{
    /// <summary>
    /// Представляет контекст данных приложения
    /// </summary>
    public class RocketContext : DbContext
    {
        /// <summary>
        /// Создает новый экземпляр контекста данных
        /// </summary>
        public RocketContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<RocketContext>(null);
        }

        /// <summary>
        /// Возвращает или задает DbSet фильмов
        /// </summary>
        public IDbSet<DbFilm> Films { get; set; }

        /// <summary>
        /// Возвращает или задает DbSet сериалов
        /// </summary>
        public IDbSet<DbTVSeries> TVSerials { get; set; }

        /// <summary>
        /// Возвращает или задает DbSet музыки
        /// </summary>
        public IDbSet<DbMusic> Musics { get; set; }

        /// <summary>
        /// Этот метод вызывается, когда модель для производного контекста данных была инициализирована,
        /// но до того, как модель была заблокирована и использована для инициализации этого контекста.
        /// </summary>
        /// <param name="modelBuilder">Построитель, который определяет модель для создаваемого контекста.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DbFilmConfiguration());
            modelBuilder.Configurations.Add(new DbTVSeriesConfiguration());
            modelBuilder.Configurations.Add(new DbMusicConfiguration());
        }
    }
}