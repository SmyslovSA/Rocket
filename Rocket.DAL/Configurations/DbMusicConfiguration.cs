using Rocket.DAL.Common.DbModels;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
	/// <summary>
	/// Конфигурация хранения данных о музыкальном релизе
	/// </summary>
	public class DbMusicConfiguration : EntityTypeConfiguration<DbMusic>
	{
		public DbMusicConfiguration()
		{
			ToTable("Music")
				.HasKey(f => f.Id)
				.Property(f => f.Id)
				.HasColumnName("Id");

			Property(f => f.ReleaseDate)
				.IsRequired()
				.HasColumnName("ReleaseDate");

			Property(f => f.Title)
				.IsRequired()
				.HasColumnName("Title")
				.HasMaxLength(50);

			Property(f => f.PosterImagePath)
				.IsOptional()
				.HasColumnName("PosterImagePath")
				.HasMaxLength(200);

			Property(f => f.Duration)
				.IsOptional()
				.HasColumnName("Duration");

			HasMany(f => f.Musicians)
				.WithMany(p => p.Musics)
				.Map(m =>
				{
					m.ToTable("MusicMusicians");
					m.MapLeftKey("MusicId");
					m.MapRightKey("MusiciansId");
				});

			HasMany(f => f.Genres)
				.WithMany(p => p.DbMusics)
				.Map(m =>
				{
					m.ToTable("MusicReleaseGenres");
					m.MapLeftKey("MusicId");
					m.MapRightKey("MusicGenreId");
				});

			HasMany(f => f.MusicTracks)
				.WithRequired(p => p.DbMusic)
				.HasForeignKey(p => p.DbMusicId);
		}
	}
}
