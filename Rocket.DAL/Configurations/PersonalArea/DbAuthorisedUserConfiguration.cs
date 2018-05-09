using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.PersonalArea
{
    public class DbAuthorisedUserConfiguration : EntityTypeConfiguration<DbAuthorisedUser>
    {
        public DbAuthorisedUserConfiguration()
        {
            ToTable("Users").
                HasKey(k => k.Id).
                Property(p => p.Id).
                HasColumnName("Id");

            Property(p => p.DbAccountId).
                HasColumnName("DbAccountId");

            Property(p => p.DbPersonalityId).
                HasColumnName("DbPersonalityId");

            Property(p => p.Avatar).
                IsOptional().
                HasColumnName("AvatarPath").
                IsUnicode().
                HasMaxLength(200).
                IsVariableLength();

            HasMany(p => p.Email).
                WithRequired(e => e.User).
                HasForeignKey(e => e.UserId);

            HasMany(p => p.Genres).
                WithMany(e => e.Users).
                Map(m => {
                    m.ToTable("UserGenres").
                    MapLeftKey("UserId").
                    MapRightKey("GenreId");
                });
        }
    }
}
