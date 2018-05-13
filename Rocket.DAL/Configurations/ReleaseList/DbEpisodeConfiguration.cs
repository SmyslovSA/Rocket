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
            ToTable("Episodes");

            Property(e => e.Number)
                .IsRequired()
                .HasColumnName("Number");

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
