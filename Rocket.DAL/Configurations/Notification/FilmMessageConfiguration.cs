using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения данных по сообщению о релизе фильма
    /// </summary>
    public class FilmMessageConfiguration: EntityTypeConfiguration<DbFilmMessage>
    {
        public FilmMessageConfiguration()
        {
            ToTable("FilmMessages");

            HasKey(x => x.Id);

            HasMany(x => x.ReceiversJoinFilms)
                .WithRequired(x => x.FilmMessage)
                .HasForeignKey(x => x.FilmMessageId);

            Property(x => x.FilmId).IsRequired();

            Property(x => x.Title).IsRequired();

            Property(x => x.ReleaseDate).IsRequired();

            Property(x => x.CreationTime).IsRequired();
        }
    }
}