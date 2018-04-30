using System.Data.Entity;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Configurations;

namespace Rocket.DAL.Context
{
    public class RocketContext : DbContext
    {
        public RocketContext() : base("DefaultConnection")
        {
            Database.SetInitializer<RocketContext>(null);
        }

        public DbSet<ResourceEntity> Resources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ResourceMap());
        }
    }
}