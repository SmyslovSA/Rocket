using Rocket.DAL.Common.DbModels.Parser;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.Parser
{
    public class SeasonEntityMap : EntityTypeConfiguration<SeasonEntity>
    {
        public SeasonEntityMap()
        {
            ToTable("Season", "seria")
                .HasKey(p => p.Id);

            Property(p => p.Number)
                .IsRequired()
                .HasColumnName("Number");

            Property(p => p.PosterImageUrl)
                .IsOptional()
                .HasColumnName("PosterImageUrl")
                .IsMaxLength();

            Property(p => p.TvSeriesId)
                .IsRequired()
                .HasColumnName("TvSeriesId");
        }
    }
}
