using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения данных email адреса получателя
    /// </summary>
    public class EmailConfiguration: EntityTypeConfiguration<DbEmail>
    {
        public EmailConfiguration()
        {
            ToTable("Emails");

            HasKey(x => x.Id);

            Property(x => x.EmailTitle).IsRequired();
        }
    }
}