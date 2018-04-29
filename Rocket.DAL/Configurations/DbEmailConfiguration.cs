using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    class DbEmailConfiguration : EntityTypeConfiguration<DbEmail>
    {
        public DbEmailConfiguration()
        {
                 ToTable("Email")
                .HasKey(e => e.Id)
                .Property(f => f.Id)
                .HasColumnName("Id");

                  Property(f => f.Name)
                 .IsRequired()
                 .HasColumnName("Name");

           
        }


        

    }
}
