using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    public class DbPersonalizedTapeConfiguration : EntityTypeConfiguration<DbPersonalizedTape>
    {
        public DbPersonalizedTapeConfiguration()
        {
            ToTable("PersonalizedTape").
                HasKey(k => k.Id).
                Property(p => p.Id).
                HasColumnName("Id");

            Property(p => p.UserId).
                HasColumnName("UserId");

            Property(p => p.CategoryId).
                HasColumnName("CategoryId");

            Property(p => p.GenreId).
                HasColumnName("GenreId");
        }
    }
}
