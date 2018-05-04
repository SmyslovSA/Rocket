using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.ReleaseList
{
    /// <summary>
    /// Конфигурация хранения данных о сериях сериалов
    /// </summary>
    public class DbEpisodeConfiguration : EntityTypeConfiguration<DbEpisode>
    {
        public DbEpisodeConfiguration()
        {
            ToTable("Episodes")
                .HasKey(e => e.Id)
                .Property(e => e.Id)
                .HasColumnName("Id");

            Property(e => e.ReleaseDate)
                .IsRequired()
                .HasColumnName("ReleaseDate");

            Property(e => e.Number)
                .IsRequired()
                .HasColumnName("Number");

            Property(e => e.Title)
                .IsOptional()
                .HasColumnName("Title")
                .HasMaxLength(50);

            Property(e => e.Duration)
                .IsOptional()
                .HasColumnName("Duration");

            Property(e => e.Summary)
                .IsOptional()
                .HasColumnName("Summary");

            Property(e => e.DbSeasonId)
                .HasColumnName("SeasonId");
        }
    }
}
