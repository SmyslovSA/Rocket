using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
   public class DbUserConfiguration: EntityTypeConfiguration<DbUser>
    {
        public DbUserConfiguration()
        {
            ToTable("UserData")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("Id");

            Property(t => t.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasMaxLength(50);

            Property(t => t.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasMaxLength(50);

            Property(t => t.Login)
                .IsRequired()
                .HasColumnName("Login")
                .HasMaxLength(50);

            Property(t => t.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasMaxLength(50);

            Property(t => t.Avatar)
                .IsOptional()
                .HasColumnName("Avatar")
                .HasMaxLength(200)
                .IsVariableLength();

            HasMany(e => e.Email)
                .WithMany(e => e.DbUser)
                .HasForeignKey(u => u.EmailId);
        }
    }
}

