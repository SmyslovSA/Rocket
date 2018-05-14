using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Notification;

namespace Rocket.DAL.Configurations.Notification
{
    /// <summary>
    /// Конфигурация хранения данных по сообщениям о платежах
    /// </summary>
    public class BillingsConfiguration : EntityTypeConfiguration<DbBillingMessage>
    {
        public BillingsConfiguration()
        {
            ToTable("BillingMessages");

            HasKey(x => x.Id);

            Property(x => x.Sum).IsRequired();

            Property(x => x.CreationTime).IsRequired();
        }
    }
}