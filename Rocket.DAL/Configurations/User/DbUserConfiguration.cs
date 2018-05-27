using System;
using Rocket.DAL.Common.DbModels.User;
using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Properties;

namespace Rocket.DAL.Configurations.User
{
    /// <summary>
    /// Конфигурация хранения данных о пользователях.
    /// </summary>
    public class DbUserConfiguration : EntityTypeConfiguration<DbUser>
    {
        public DbUserConfiguration()
        {
            ToTable("Users")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("Id");

            Property(t => t.FirstName)
                .IsOptional()
                .HasColumnName("FirstName")
                .HasMaxLength(Convert.ToInt32(Resources.MAXFIRSTNAMELENGHT));

            Property(t => t.LastName)
                .IsOptional()
                .HasColumnName("LastName")
                .HasMaxLength(Convert.ToInt32(Resources.MAXLASTNAMELENGHT));

            Property(t => t.Login)
                .IsRequired()
                .HasColumnName("Login")
                .HasMaxLength(Convert.ToInt32(Resources.MAXLOGINLENGHT));

            Property(t => t.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasMaxLength(Convert.ToInt32(Resources.MAXPASSWORDLENGHT));

            HasOptional(s => s.AccountStatus)
                .WithMany(st => st.DbUsers);

            HasOptional(s => s.AccountLevel)
                .WithMany(st => st.DbUsers);

            HasMany(f => f.Roles)
                .WithMany(c => c.Users)
                .Map(m =>
                {
                    m.ToTable("UsersRoles");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });

            HasOptional(ud => ud.UserDetail)
                .WithRequired(u => u.User);
        }
    }
}