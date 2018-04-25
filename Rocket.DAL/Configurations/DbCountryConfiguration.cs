using Rocket.DAL.Common.DbModels;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    /// <summary>
    /// Конфигурация хранения данных о странах
    /// </summary>
    public class DbCountryConfiguration : EntityTypeConfiguration<DbCountry>
    {
        public DbCountryConfiguration()
        {
            ToTable("Countries")
                .HasKey(c => c.Id)
                .Property(c => c.Id)
                .HasColumnName("Id");

            Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50);
        }
    }
}
