using Rocket.DAL.Common.DbModels.DbPersonalArea;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    public class DbCategoryConfiguration : EntityTypeConfiguration<DbCategory>
    {
        public DbCategoryConfiguration()
        {
            ToTable("Category").
                HasKey(k => k.Id).
                Property(p => p.Id).
                HasColumnName("Id");

            Property(p => p.Name).
                IsRequired().
                HasMaxLength(30).
                IsVariableLength().
                HasColumnName("Name");

            HasMany(g => g.Genres).
                WithMany(c => c.Categories).
                Map(m =>
                {
                    m.ToTable("CategoriesGenres").
                    MapLeftKey("CategoryId").
                    MapRightKey("GenreId");
                });
        }
    }
}
