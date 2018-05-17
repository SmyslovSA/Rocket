using Rocket.DAL.Common.DbModels.User;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.User
{
    /// <summary>
    /// Конфигурация хранения данных о пользователях
    /// </summary>
    public class DbAccountStatusConfiguration : EntityTypeConfiguration<DbAccountStatus>
    {
        public DbAccountStatus()
        {
            ToTable("AccountStatuses")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("Id");

            Property(t => t.Name)
                .IsOptional()
                .HasColumnName("Name")
                .HasMaxLength(30);

            HasMany<DbUser>(t => t.DbUsers)
                .WithRequired(p => p.AccountStatus)
                .Map(m =>
                {
                    m.ToTable("TVSerialsDirectors");
                    m.MapLeftKey("TVSeriesId");
                    m.MapRightKey("DirectorId");
                });

            HasMany(t => t.Db)
                .WithRequired(s => s.DbTVSeries)
                .HasForeignKey(s => s.DbTVSeriesId);
        }
    }
}