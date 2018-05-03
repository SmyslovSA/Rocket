using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.PersonalArea
{
    public class DbUserConfiguration : EntityTypeConfiguration<DbUser>
    {
        public DbUserConfiguration()
        {
            ToTable("Users").
                HasKey(k => k.Id).
                Property(p => p.Id).
                HasColumnName("Id");

            Property(p => p.FirstName).
                IsOptional().
                HasColumnName("FirstName").
                IsUnicode().
                HasMaxLength(30).
                IsVariableLength();

            Property(p => p.LastName).
                IsOptional().
                HasColumnName("LastName").
                IsUnicode().
                HasMaxLength(50).
                IsVariableLength();

            Property(p => p.Login).
                IsRequired().
                HasColumnName("Login").
                IsUnicode().
                HasMaxLength(30).
                IsVariableLength();

            Property(p => p.Password).
                IsRequired().
                HasColumnName("Password").
                IsUnicode().
                HasMaxLength(30).
                IsVariableLength();

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
