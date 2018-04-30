using System.Data.Entity;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.DAL.Common.Context
{
    public interface IRocketContext : IDbContext
    {
        DbSet<ResourceEntity> Resources { get; set; }
    }
}
