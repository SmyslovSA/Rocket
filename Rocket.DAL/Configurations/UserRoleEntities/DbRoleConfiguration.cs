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
        }

    }
}
