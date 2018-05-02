using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    class DbGenreConfiguration: EntityTypeConfiguration<DbGenre>
    {
        public DbGenreConfiguration()
        {
            ToTable("Genre")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("Id");
            Property(t => t.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50);

        }
       
    }
}
