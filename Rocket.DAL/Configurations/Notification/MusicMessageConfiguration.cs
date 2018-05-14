using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения данных по сообщению о музыкальном релизе
    /// </summary>
    public class MusicMessageConfiguration: EntityTypeConfiguration<DbMusicMessage>
    {
        public MusicMessageConfiguration()
        {
            ToTable("MusicMessages");

            HasKey(x => x.Id);

            HasMany(x => x.ReceiversJoinMusics)
                .WithRequired(x => x.MusicMessage)
                .HasForeignKey(x => x.MusicMessageId);

            Property(x => x.MusicId).IsRequired();

            Property(x => x.Title).IsRequired();

            Property(x => x.ReleaseDate).IsRequired();

            HasMany(x => x.Performers)
                .WithRequired(x => x.MusicMessage)
                .HasForeignKey(x => x.MusicMessageId);

            Property(x => x.CreationTime).IsRequired();
        }
    }
}