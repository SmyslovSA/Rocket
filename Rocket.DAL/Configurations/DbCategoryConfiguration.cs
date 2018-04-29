using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;


namespace Rocket.DAL.Configurations
{
    class DbCategoryConfiguration: EntityTypeConfiguration<DbCategory>
    {
        public DbCategoryConfiguration()
        {

            ToTable("Category")
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
