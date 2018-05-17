using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.PersonalArea
{
    public class DbAuthorisedUserConfiguration : EntityTypeConfiguration<DbAuthorisedUser>
    {
        public DbAuthorisedUserConfiguration()
        {
            ToTable("AuthorisedUsers")
                .HasRequired(p => p.DbUser)
                .WithRequiredDependent(d => d.DbAuthorisedUser)
                .Map(m => m.MapKey("DbUserId"));

            Property(p => p.Avatar)
                .IsOptional()
                .HasColumnName("AvatarPath")
                .IsUnicode()
                .HasMaxLength(200)
                .IsVariableLength();

            HasMany(p => p.Email)
                .WithRequired(e => e.DbAuthorisedUser)
                .HasForeignKey(e => e.DbAuthorisedUserId);

            HasMany(p => p.Genres)
                .WithMany(e => e.AuthorisedUsers)
                .Map(m =>
                {
                    m.ToTable("AuthorisedUserGenres")
                    .MapLeftKey("AuthorisedUserId")
                    .MapRightKey("GenreId");
                });
        }
    }
}