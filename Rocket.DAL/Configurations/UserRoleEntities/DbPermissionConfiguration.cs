using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Identity;

namespace Rocket.DAL.Configurations.UserRoleEntities
{
    // Конфигурация хранения данных прав доступа (пермишенов)
    public class DbPermissionConfiguration : EntityTypeConfiguration<DbPermission>
    {
        public DbPermissionConfiguration()
        {
            ToTable("DBPermission")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("permission_id");

            Property(t => t.Description)
                .IsOptional()
                .HasColumnName("description")
                .HasMaxLength(250)
                .IsVariableLength();

            Property(t => t.ValueName)
                .IsRequired()
                .HasColumnName("value_name")
                .HasMaxLength(50)
                .IsVariableLength();
        }
    }
}