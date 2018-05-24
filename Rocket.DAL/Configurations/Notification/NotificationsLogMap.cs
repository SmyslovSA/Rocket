using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    public class NotificationsLogMap : EntityTypeConfiguration<NotificationsLogEntity>
    {
        public NotificationsLogMap()
        {
            ToTable("NotificationsLog")
                .HasKey(p => p.Id);

            Property(p => p.NotificationType)
                .IsRequired()
                .HasColumnName("NotificationType");

            Property(p => p.UserId)
                .IsRequired()
                .HasColumnName("UserId");

            Property(p => p.ReleaseType)
                .IsRequired()
                .HasColumnName("ReleaseType");

            Property(p => p.ReleaseId)
                .IsRequired()
                .HasColumnName("ReleaseId");

            Property(p => p.CreatedDateTime)
                .IsRequired()
                .HasColumnName("CreatedDateTime");
        }
    }
}
