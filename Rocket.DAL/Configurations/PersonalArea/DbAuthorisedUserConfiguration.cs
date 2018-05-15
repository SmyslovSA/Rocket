using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.PersonalArea
{
    public class DbAuthorisedUserConfiguration : EntityTypeConfiguration<DbAuthorisedUser>
    {
        public DbAuthorisedUserConfiguration()
        {
            ToTable("AuthorisedUsers").
                HasKey(k => k.Id).
                Property(p => p.Id).
                HasColumnName("Id");

            Property(p => p.DbUser.FirstName).
                HasColumnName("FirstName").
                IsOptional().
                IsUnicode().
                HasMaxLength(50).
                IsVariableLength();

            Property(p => p.DbUser.LastName).
                HasColumnName("LastName").
                IsOptional().
                IsUnicode().
                HasMaxLength(50).
                IsVariableLength();

            Property(p => p.DbUser.Login).
                HasColumnName("Login").
                IsRequired().
                HasMaxLength(30).
                IsVariableLength();

            Property(p => p.DbUser.Password).
                HasColumnName("Password").
                IsRequired().
                HasMaxLength(50).
                IsVariableLength();

            Property(p => p.Avatar).
                IsOptional().
                HasColumnName("AvatarPath").
                IsUnicode().
                HasMaxLength(200).
                IsVariableLength();

            HasMany(p => p.Email).
                WithRequired(e => e.DbAuthorisedUser).
                HasForeignKey(e => e.DbAuthorisedUserId);

            HasMany(p => p.Genres).
                WithMany(e => e.AuthorisedUsers).
                Map(m =>
                {
                    m.ToTable("AuthorisedUserGenres").
                    MapLeftKey("AuthorisedUserId").
                    MapRightKey("GenreId");
                });
        }
    }
}
