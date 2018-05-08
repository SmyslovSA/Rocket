using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.DbUser;

namespace Rocket.DAL.Configurations.UserRoleEntities
{
    // пока нету единого юзера - костыль для DbModels.DbUser; он ближе к правде
    public class DbUserConfiguration : EntityTypeConfiguration<DbUser>
    {
        public DbUserConfiguration()
        {
            ToTable("Users")
                .HasKey(k => k.Id)
                .Property(p => p.Id)
                .HasColumnName("Id");

            HasMany(p => p.Roles)
                .WithMany(e => e.Users)
                .Map(m =>
                {
                    m.ToTable("t_user_detl_role")
                        .MapLeftKey("UserId")
                        .MapRightKey("RoleId");
                });
        }
    }
}
