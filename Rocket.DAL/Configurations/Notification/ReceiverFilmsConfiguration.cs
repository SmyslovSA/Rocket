using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения сводных данных о получателе и релизе фильма
    /// </summary>
    public class ReceiverFilmsConfiguration: EntityTypeConfiguration<ReceiversJoinFilms>
    {
        public ReceiverFilmsConfiguration()
        {
            ToTable("ReceiversJoinFilms");

            HasKey(x => new {x.ReceiverId, x.FilmMessageId});

            Property(x => x.Viewed).IsRequired();
        }
    }
}