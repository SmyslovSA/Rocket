using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Identity;

namespace Rocket.DAL.Configurations.UserRoleEntities
{
    public class DbUserLoginConfiguration : EntityTypeConfiguration<DbUserLogin>
    {
        public DbUserLoginConfiguration()
        {
            ToTable("t_user_logins")
                .HasKey(k => new
                {
                    k.LoginProvider, k.ProviderKey, k.UserId
                });

            HasRequired(t => t.User)
                .WithMany(t => t.Logins)
                .HasForeignKey(t => t.UserId);
        }
    }
}