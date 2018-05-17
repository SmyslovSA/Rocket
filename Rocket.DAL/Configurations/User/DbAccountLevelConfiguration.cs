using Rocket.DAL.Common.DbModels.User;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.User
{
    /// <summary>
    /// Конфигурация хранения данных об уровне пользователя.
    /// </summary>
    public class DbAccountLevelConfiguration : EntityTypeConfiguration<DbAccountLevel>
    {
        public DbAccountLevelConfiguration()
        {
            ToTable("AccountLevels")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("Id");

            Property(t => t.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(30);

            HasMany<DbUser>(t => t.DbUsers)
                .WithOptional(a => a.AccountLevel);
        }
    }
}