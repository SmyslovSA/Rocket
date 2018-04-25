using Rocket.DAL.Common.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.DAL.Configurations
{
    /// <summary>
    /// Конфигурация хранения данных о жанрах видео (фильмов, сериалов)
    /// </summary>
    public class DbVideoGenreConfiguration : EntityTypeConfiguration<DbVideoGenre>
    {
        public DbVideoGenreConfiguration()
        {
            ToTable("VideoGenres")
                .HasKey(v => v.Id)
                .Property(v => v.Id)
                .HasColumnName("Id");

            Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50);
        }
    }
}
