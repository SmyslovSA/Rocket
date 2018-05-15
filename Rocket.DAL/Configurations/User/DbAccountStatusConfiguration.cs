using Rocket.DAL.Common.DbModels.User;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.User
{
    /// <summary>
    /// Конфигурация хранения данных о пользователях
    /// </summary>
    public class DbUserConfiguration : EntityTypeConfiguration<DbUser>
    {
        public DbUsersConfiguration()
        {
            ToTable("Users")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("Id");

            Property(t => t.FirstName)
                .IsOptional()
                .HasColumnName("FirstName")
                .HasMaxLength(35);

            Property(t => t.LastName)
                .IsOptional()
                .HasColumnName("LastName")
                .HasMaxLength(35);

            Property(t => t.Login)
                .IsRequired()
                .HasColumnName("Login")
                .HasMaxLength(50);

            Property(t => t.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasMaxLength(50);

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