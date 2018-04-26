using Rocket.DAL.Common.DbModels;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    /// <summary>
    /// Конфигурация хранения данных о сезонах сериалов
    /// </summary>
    public class DbSeasonConfiguration : EntityTypeConfiguration<DbSeason>
    {
        public DbSeasonConfiguration()
        {
            ToTable("Seasons")
                .HasKey(s => s.Id)
                .Property(s => s.Id)
                .HasColumnName("Id");

            Property(s => s.Number)
                .IsRequired()
                .HasColumnName("Number");

            Property(s => s.PosterImagePath)
                .IsOptional()
                .HasColumnName("PosterImagePath")
                .HasMaxLength(200);

            Property(s => s.Summary)
                .IsOptional()
                .HasColumnName("Summary");

            Property(s => s.DbTVSeriesId)
                .HasColumnName("TVSeriesId");

            HasMany(s => s.Episodes)
                .WithRequired(e => e.DbSeason)
                .HasForeignKey(e => e.DbSeasonId);
        }
    }
}
