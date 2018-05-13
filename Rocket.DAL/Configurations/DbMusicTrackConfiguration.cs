using Rocket.DAL.Common.DbModels;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
	public class DbMusicTrackConfiguration : EntityTypeConfiguration<DbMusicTrack>
	{
		public DbMusicTrackConfiguration()
		{
			ToTable("MusicTrack")
				.HasKey(v => v.Id)
				.Property(v => v.Id)
				.HasColumnName("Id");

			Property(c => c.Duration)
				.IsOptional()
				.HasColumnName("Duration");

			Property(c => c.Title)
				.IsRequired()
				.HasColumnName("Title")
				.HasMaxLength(200);

			Property(p => p.DbMusicId)
				.HasColumnName("MusicId");
		}
	}
}
