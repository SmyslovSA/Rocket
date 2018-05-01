using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.DAL.Configurations
{
    /// <summary>
    /// Описание сущности ResourceItemEntity
    /// </summary>
    public class ResourceItemMap : EntityTypeConfiguration<ResourceItemEntity>
    {
        public ResourceItemMap()
        {
            ToTable("Resource Item")
                .HasKey(p => p.Id);

            Property(p => p.ResourceId)
                .IsRequired()
                .HasColumnName("Resource Id");

            Property(p => p.ResourceInternalId)
                .IsRequired()
                .HasColumnName("Resource Internal Id")
                .HasMaxLength(50);

            Property(p => p.CreatedDateTime)
                .IsRequired()
                .HasColumnName("Created Date Time");

            Property(p => p.LastModified)
                .IsRequired()
                .HasColumnName("Last Modified");

            this.HasRequired<ResourceEntity>(p => p.Resource).WithMany(r => r.ResourceItems)
                .HasForeignKey<int>(p => p.ResourceId);
        }
    }
}
