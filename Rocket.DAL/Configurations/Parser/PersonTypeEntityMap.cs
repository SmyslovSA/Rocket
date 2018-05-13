using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.DAL.Configurations.Parser
{
    public class PersonTypeEntityMap : EntityTypeConfiguration<PersonTypeEntity>
    {
        public PersonTypeEntityMap()
        {
            ToTable("PersonType", "seria")
                .HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(250);
        }
    }
}
