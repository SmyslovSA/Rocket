using System.Data.Entity;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Configurations;

namespace Rocket.DAL.Context
{
    public class RocketContext : DbContext
    {
        public RocketContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RocketContext, Migrations.Configuration>());
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
        /// DbSet настроек парсера
        /// </summary>
        public DbSet<DbMusic> DbMusics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ResourceMap());
            modelBuilder.Configurations.Add(new ParserSettingsMap());
            modelBuilder.Configurations.Add(new ResourceItemMap());
            modelBuilder.Configurations.Add(new DbMusicConfiguration());
        }
    }
}