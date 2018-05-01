using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.DAL.Configurations.UserRoleEntities
{
    // Конфигурация хранения данных прав доступа 
    // и функциональных возможностей (пермишенов)
    public class DbPermissionConfiguration : EntityTypeConfiguration<DbPermission>
    {
        public DbPermissionConfiguration()
        {
            ToTable("t_user_permission")
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
                .HasColumnName("valueName")
                .HasMaxLength(50)
                .IsVariableLength();

            // уточнить является ли это дублированием
            //HasMany(t => t.Roles)
            //    .WithMany(x => x.Permissions);
        }
    }
}