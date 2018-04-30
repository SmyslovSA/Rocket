using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.DAL.Configurations
{
    /// <summary>
    /// Описание сущности Resource
    /// </summary>
    public class ResourceMap : EntityTypeConfiguration<ResourceEntity>
    {
        public ResourceMap()
        {
            ToTable("Resource")
                .HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50);

            Property(p => p.ResourceLink)
                .IsRequired()
                .HasColumnName("Resource Link")
                .HasMaxLength(150);
        }
    }
}
