using Rocket.DAL.Common.Repositories.Temp;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Rocket.DAL.Context;

namespace Rocket.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly RocketContext _rocketContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(RocketContext rocketContext)
        {
            _rocketContext = rocketContext;
            _dbSet = _rocketContext.Set<TEntity>();
        }

        public virtual TEntity Find<Tkey>(params Tkey[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery<Tkey>(string query, params Tkey[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Attach(entity);
            _rocketContext.Entry(entity).State = EntityState.Added;
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _rocketContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<Tkey>(Tkey id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Attach(entity);
            _rocketContext.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }
    }
}
