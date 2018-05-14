using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения сводных данных о получателе и релизе сериала
    /// </summary>
    public class ReceiverTVSeriesConfiguration: EntityTypeConfiguration<ReceiversJoinTVSeries>
    {
        public ReceiverTVSeriesConfiguration()
        {
            ToTable("ReceiversJoinTVSeries");

            HasKey(x => new { x.ReceiverId, x.TVSeriesMessageId });

            Property(x => x.Viewed).IsRequired();
        }
    }
}