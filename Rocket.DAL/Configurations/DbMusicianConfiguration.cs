using Rocket.DAL.Common.DbModels;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
	/// <summary>
	/// Конфигурация хранения данных о музыкальных исполнителях
	/// </summary>
	class DbMusicianConfiguration : EntityTypeConfiguration<DbMusician>
	{
		public DbMusicianConfiguration()
		{
			ToTable("Musician")
				.HasKey(v => v.Id)
				.Property(v => v.Id)
				.HasColumnName("Id");

			Property(c => c.FullName)
				.IsRequired()
				.HasColumnName("FullName");

		}
	}
}