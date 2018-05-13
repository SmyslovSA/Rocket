using Rocket.DAL.Common.DbModels.Parser;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.Parser
{
    public class GenreEntityMap : EntityTypeConfiguration<GenreEntity>
    {
        public GenreEntityMap()
        {
            ToTable("Genre", "seria")
                .HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(250);
        }
    }
}
