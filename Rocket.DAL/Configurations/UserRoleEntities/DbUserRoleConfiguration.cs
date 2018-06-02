using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Identity;

namespace Rocket.DAL.Configurations.UserRoleEntities
{
    public class DbUserRoleConfiguration : EntityTypeConfiguration<DbUserRole>
    {
        public DbUserRoleConfiguration()
        {
            ToTable("t_users_roles");

            HasRequired(t => t.User)
                .WithMany(t => t.Roles)
                .HasForeignKey(t => t.UserId);

            HasRequired(t => t.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(t => t.RoleId);
        }
    }
}