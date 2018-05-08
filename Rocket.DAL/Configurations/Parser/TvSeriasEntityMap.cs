using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.DAL.Configurations.Parser
{
    public class TvSeriasEntityMap : EntityTypeConfiguration<TvSeriasEntity>
    {
        public TvSeriasEntityMap()
        {
            ToTable("TvSerias")
                .HasKey(p => p.Id);

            Property(p => p.TitleRu)
                .IsRequired()
                .HasColumnName("Title Ru")
                .HasMaxLength(250);

            //Property(p => p.Prefix)
            //    .IsOptional()
            //    .HasColumnName("Prefix")
            //    .HasMaxLength(200);

            //this.HasRequired<ResourceEntity>(p => p.Resource).WithMany(r => r.ParserSettings)
            //    .HasForeignKey<int>(p => p.ResourceId);
        }
    }
}
