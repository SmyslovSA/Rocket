using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Migrations;
using System.Data.Entity;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Configurations;
using Rocket.DAL.Configurations.Parser;
using Rocket.DAL.Configurations.ReleaseList;
using Rocket.DAL.Configurations.User;

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
        public RocketContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RocketContext, Configuration>());
        }

        /// <summary>
        /// DbSet ресурсов для парсинга
        /// </summary>
        public DbSet<ResourceEntity> Resources { get; set; }

        /// <summary>
        /// DbSet настроек парсера
        /// </summary>
        public DbSet<ParserSettingsEntity> ParserSettings { get; set; }

        /// <summary>
        /// DbSet элемента ресурса
        /// </summary>
        public DbSet<ResourceItemEntity> ResourceItems { get; set; }

        /// <summary>
        /// DbSet музыкального релиза
        /// </summary>
        public DbSet<DbMusic> DbMusics { get; set; }

        /// <summary>
        /// DbSet жанра
        /// </summary>
        public DbSet<DbMusicGenre> DbMusicGenres { get; set; }

        /// <summary>
        /// DbSet музыкального трека
        /// </summary>
        public DbSet<DbMusicTrack> DbMusicTracks { get; set; }

        /// <summary>
        /// Набор сущностей категорий.
        /// </summary>
        public DbSet<CategoryEntity> CategoryEntities { get; set; }

        public DbSet<TvSeriasEntity> TvSeriasEntities { get; set; }

        public DbSet<PersonTypeEntity> PersonTypeEntities { get; set; }

        public DbSet<GenreEntity> GenreEntities { get; set; }

        public DbSet<PersonEntity> PersonEntities { get; set; }

        public DbSet<EpisodeEntity> EpisodeEntities { get; set; }

        public DbSet<SeasonEntity> SeasonEntities { get; set; }

        /// <summary>
        /// Этот метод вызывается, когда модель для производного контекста данных была инициализирована,
        /// но до того, как модель была заблокирована и использована для инициализации этого контекста.
        /// </summary>
        /// <param name="modelBuilder">Построитель, который определяет модель для создаваемого контекста.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ResourceMap());
            modelBuilder.Configurations.Add(new ParserSettingsMap());
            modelBuilder.Configurations.Add(new ResourceItemMap());
            modelBuilder.Configurations.Add(new DbMusicConfiguration());
            modelBuilder.Configurations.Add(new DbMusicGenreConfiguration());
            modelBuilder.Configurations.Add(new DbMusicTrackConfiguration());
            modelBuilder.Configurations.Add(new DbMusicianConfiguration());

            modelBuilder.Configurations.Add(new CategoryEntityMap());
            modelBuilder.Configurations.Add(new TvSeriasEntityMap());
            modelBuilder.Configurations.Add(new PersonTypeEntityMap());
            modelBuilder.Configurations.Add(new GenreEntityMap());
            modelBuilder.Configurations.Add(new PersonEntityMap());
            modelBuilder.Configurations.Add(new EpisodeEntityMap());
            modelBuilder.Configurations.Add(new SeasonEntityMap());
        }
    }
}