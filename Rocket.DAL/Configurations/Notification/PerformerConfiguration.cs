using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения данных об исполнителе музыкального релиза
    /// </summary>
    public class PerformerConfiguration: EntityTypeConfiguration<DbPerformer>
    {
        public PerformerConfiguration()
        {
            ToTable("Performers");

            HasKey(x => x.MusicianId);

            Property(x => x.Name).IsRequired();
        }
    }
}