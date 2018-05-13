using Rocket.DAL.Common.DbModels.Parser;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.Parser
{
    public class PersonEntityMap : EntityTypeConfiguration<PersonEntity>
    {
        public PersonEntityMap()
        {
            ToTable("Person", "seria")
                .HasKey(p => p.Id);

            Property(p => p.FullNameRu)
                .IsRequired()
                .HasColumnName("FullNameRu")
                .HasMaxLength(250);

            Property(p => p.FullNameEn)
                .IsRequired()
                .HasColumnName("FullNameEn")
                .HasMaxLength(250);

            Property(p => p.LostfilmPersonalPageUrl)
                .IsRequired()
                .HasColumnName("LostfilmPersonalPageUrl")
                .IsMaxLength();

            Property(p => p.PhotoThumbnailUrl)
                .IsOptional()
                .HasColumnName("PhotoThumbnailUrl")
                .IsMaxLength();
        }
    }
}
