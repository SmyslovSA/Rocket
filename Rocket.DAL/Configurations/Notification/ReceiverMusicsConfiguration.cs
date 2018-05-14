using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения сводных данных о получателе и музыкальном релизе
    /// </summary>
    public class ReceiverMusicsConfiguration: EntityTypeConfiguration<ReceiversJoinMusics>
    {
        public ReceiverMusicsConfiguration()
        {
            ToTable("ReceiversJoinMusics");

            HasKey(x => new { x.ReceiverId, x.MusicMessageId });

            Property(x => x.Viewed).IsRequired();
        }
    }
}