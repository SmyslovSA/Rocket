using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.ReleaseList
{
    /// <summary>
    /// Конфигурация хранения данных о сериалах
    /// </summary>
    public class DbTVSeriesConfiguration : EntityTypeConfiguration<DbTVSeries>
    {
        public DbTVSeriesConfiguration()
        {
            ToTable("TVSerials")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("Id");

            Property(t => t.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasMaxLength(50);

            Property(t => t.PosterImagePath)
                .IsOptional()
                .HasColumnName("PosterImagePath")
                .HasMaxLength(200);

            Property(t => t.Summary)
                .IsOptional()
                .HasColumnName("Summary");

            HasMany(t => t.Directors)
                .WithMany(p => p.DbTVSerialsDirector)
                .Map(m =>
                {
                    m.ToTable("TVSerialsDirectors");
                    m.MapLeftKey("TVSeriesId");
                    m.MapRightKey("DirectorId");
                });

            HasMany(t => t.Cast)
                .WithMany(p => p.DbTVSerialsActor)
                .Map(m =>
                {
                    m.ToTable("TVSerialsActors");
                    m.MapLeftKey("TVSeriesId");
                    m.MapRightKey("ActorId");
                });

            HasMany(t => t.Genres)
                .WithMany(g => g.DbTVSerials)
                .Map(m =>
                {
                    m.ToTable("TVSerialsVideoGenres");
                    m.MapLeftKey("TVSeriesId");
                    m.MapRightKey("VideoGenreId");
                });

            HasMany(t => t.Countries)
                .WithMany(c => c.DbTVSerials)
                .Map(m =>
                {
                    m.ToTable("TVSerialsCountries");
                    m.MapLeftKey("TVSeriesId");
                    m.MapRightKey("CountryId");
                });

            HasMany(t => t.DbSeasons)
                .WithRequired(s => s.DbTVSeries)
                .HasForeignKey(s => s.DbTVSeriesId);
        }
    }
}
