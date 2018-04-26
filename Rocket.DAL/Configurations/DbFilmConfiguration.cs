using Rocket.DAL.Common.DbModels;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    /// <summary>
    /// Конфигурация хранения данных о фильмах
    /// </summary>
    public class DbFilmConfiguration : EntityTypeConfiguration<DbFilm>
    {
        public DbFilmConfiguration()
        {
            ToTable("Films")
                .HasKey(f => f.Id)
                .Property(f => f.Id)
                .HasColumnName("Id");

            Property(f => f.ReleaseDate)
                .IsRequired()
                .HasColumnName("ReleaseDate");

            Property(f => f.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasMaxLength(50);

            Property(f => f.PosterImagePath)
                .IsOptional()
                .HasColumnName("PosterImagePath")
                .HasMaxLength(200);

            Property(f => f.Duration)
                .IsOptional()
                .HasColumnName("Duration");

            Property(f => f.Summary)
                .IsOptional()
                .HasColumnName("Summary");

            Property(f => f.TrailerLink)
                .IsOptional()
                .HasColumnName("TrailerLink");

            HasMany(f => f.Directors)
                .WithMany(p => p.DbFilmsDirector)
                .Map(m =>
                {
                    m.ToTable("FilmsDirectors");
                    m.MapLeftKey("FilmId");
                    m.MapRightKey("DirectorId");
                });

            HasMany(f => f.Cast)
                .WithMany(p => p.DbFilmsActor)
                .Map(m =>
                {
                    m.ToTable("FilmsActors");
                    m.MapLeftKey("FilmId");
                    m.MapRightKey("ActorId");
                });

            HasMany(f => f.Genres)
                .WithMany(g => g.DbFilms)
                .Map(m =>
                {
                    m.ToTable("FilmsVideoGenres");
                    m.MapLeftKey("FilmId");
                    m.MapRightKey("VideoGenreId");
                });

            HasMany(f => f.Countries)
                .WithMany(c => c.DbFilms)
                .Map(m =>
                {
                    m.ToTable("FilmsCountries");
                    m.MapLeftKey("FilmId");
                    m.MapRightKey("CountryId");
                });
        }
    }
}
