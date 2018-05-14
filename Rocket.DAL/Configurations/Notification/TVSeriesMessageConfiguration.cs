using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения данных по сообщению о релизе сериала
    /// </summary>
    public class TVSeriesMessageConfiguration: EntityTypeConfiguration<DbTVSeriesMessage>
    {
        public TVSeriesMessageConfiguration()
        {
            ToTable("TVSeriesMessages");

            HasKey(x => x.Id);

            Property(x => x.TVSeriesId).IsRequired();

            HasMany(x => x.ReceiversJoinTVSeries)
                .WithRequired(x => x.TVSeriesMessage)
                .HasForeignKey(x => x.TVSeriesMessageId);

            Property(x => x.Title).IsRequired();

            Property(x => x.SeasonNumber).IsRequired();

            Property(x => x.EpisodeNumber).IsRequired();

            Property(x => x.ReleaseDate).IsRequired();

            Property(x => x.CreationTime).IsRequired();
        }
    }
}