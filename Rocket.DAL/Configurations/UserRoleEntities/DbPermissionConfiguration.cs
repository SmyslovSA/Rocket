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
                .HasColumnName("prm_id");

        }
    }
}
