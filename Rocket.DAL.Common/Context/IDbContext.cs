using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Rocket.DAL.Common.Context
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        Database Database { get; }

        DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
