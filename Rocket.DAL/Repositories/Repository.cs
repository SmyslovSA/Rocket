using Rocket.DAL.Common.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Rocket.DAL.Context;
using System;
using System.Linq.Expressions;

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

        public virtual TEntity Find<TKey>(params TKey[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery<TKey>(string query, params TKey[] parameters)
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

        public virtual void Delete<TKey>(TKey id)
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

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}
