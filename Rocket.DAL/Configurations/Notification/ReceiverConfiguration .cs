using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения данных о получателе нотификации
    /// </summary>
    public class ReceiverConfiguration : EntityTypeConfiguration<DbReceiver>
    {
        public ReceiverConfiguration()
        {
            ToTable("Receivers");

            HasKey(x => x.Id);

            Property(x => x.UserId).IsRequired();

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(x => x.Emails)
                .WithRequired(x => x.Receiver)
                .HasForeignKey(x => x.ReceiverId);

            Property(x => x.NotifyByEmail).IsRequired();

            Property(x => x.NotifyByPush).IsRequired();

            HasMany(x => x.BillingMessages)
                .WithRequired(x => x.Receiver)
                .HasForeignKey(x => x.ReceiverId);

            HasMany(x => x.CustomMessages)
                .WithRequired(x => x.Receiver)
                .HasForeignKey(x => x.ReceiverId);

            HasMany(x => x.ReceiversJoinFilms)
                .WithRequired(x => x.Receiver)
                .HasForeignKey(x => x.ReceiverId);

            HasMany(x => x.ReceiversJoinMusics)
                .WithRequired(x => x.Receiver)
                .HasForeignKey(x => x.ReceiverId);

            HasMany(x => x.ReceiversJoinTVSeries)
                .WithRequired(x => x.Receiver)
                .HasForeignKey(x => x.ReceiverId);
        }
    }
}