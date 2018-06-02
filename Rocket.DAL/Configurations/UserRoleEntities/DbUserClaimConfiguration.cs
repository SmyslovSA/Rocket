using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Identity;

namespace Rocket.DAL.Configurations.UserRoleEntities
{
    public class DbUserClaimConfiguration : EntityTypeConfiguration<DbUserClaim>
    {
        public DbUserClaimConfiguration()
        {
            ToTable("t_user_claims")
                .HasKey(k => k.Id)
                .Property(p => p.Id)
                .HasColumnName("Id");

            HasRequired(t => t.User)
                .WithMany(t => t.Claims)
                .HasForeignKey(t => t.UserId);
        }
    }
}