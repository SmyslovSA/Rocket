using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    class DbPersonalizedTapeConfiguration :EntityTypeConfiguration<DbPersonalizedTape>
    {
        public DbPersonalizedTapeConfiguration()
        {
            ToTable("PersonalizedTape")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("Id");
            HasMany(p => p.PersonalizedTape)
                .WithRequired(p => p.User);
            HasMany(p => p.PersonalizedTape)
                .WithRequired(p => p.Genre);
            HasMany(p => p.PersonalizedTape)
                .WithRequired(p => p.Category);


        }
    }
}
