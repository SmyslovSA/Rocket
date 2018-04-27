using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.DAL.Configurations.UserRoleEntities
{
    public class DbRoleConfiguration : EntityTypeConfiguration<DbRole>
    {
        public DbRoleConfiguration()
        {
            ToTable("t_user_role")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("role_id");

            Property(t => t.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsVariableLength();

            Property(t => t.IsActive)
                .IsRequired()
                .HasColumnName("is_active");

            HasMany(t => t.Permissions)
                .WithMany(x => x.Roles);
        }
    }
}