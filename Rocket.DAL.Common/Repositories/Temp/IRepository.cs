using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.DAL.Common.Repositories.Temp
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find<TKey>(params TKey[] keyValues);

        IQueryable<TEntity> SelectQuery<TKey>(string query, params TKey[] parameters);

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete<TKey>(TKey id);

        void Delete(TEntity entity);

        IQueryable<TEntity> Queryable();

    }
}
