using Rocket.DAL.Common.DbModels.ReleaseList;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.ReleaseList
{
    /// <summary>
    /// Конфигурация хранения данных о фильмах
    /// </summary>
    public class DbUserConfiguration : EntityTypeConfiguration<DbUser>
    {
        public DbUserConfiguration()
        {
            ToTable("Users")
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
                .WithMany(p => p.DbUsersDirector)
                .Map(m =>
                {
                    m.ToTable("UsersDirectors");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("DirectorId");
                });

            HasMany(f => f.Cast)
                .WithMany(p => p.DbUsersActor)
                .Map(m =>
                {
                    m.ToTable("UsersActors");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("ActorId");
                });

            HasMany(f => f.Genres)
                .WithMany(g => g.DbUsers)
                .Map(m =>
                {
                    m.ToTable("UsersVideoGenres");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("VideoGenreId");
                });

            HasMany(f => f.Countries)
                .WithMany(c => c.DbUsers)
                .Map(m =>
                {
                    m.ToTable("UsersCountries");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("CountryId");
                });
        }
    }
}
