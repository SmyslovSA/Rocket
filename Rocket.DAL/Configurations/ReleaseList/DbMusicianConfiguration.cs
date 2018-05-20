using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.ReleaseList
{
    /// <summary>
    /// Конфигурация хранения данных о музыкальных исполнителях
    /// </summary>
    public class DbMusicianConfiguration : EntityTypeConfiguration<DbMusician>
    {
        public DbMusicianConfiguration()
        {
            ToTable("Musician")
                .HasKey(v => v.Id)
                .Property(v => v.Id)
                .HasColumnName("Id");

            Property(c => c.FullName)
                .IsRequired()
                .HasColumnName("FullName");
        }
    }
}