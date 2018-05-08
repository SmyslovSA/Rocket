using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.DAL.Common.Repositories.Temp
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find<Tkey>(params Tkey[] keyValues);

        IQueryable<TEntity> SelectQuery<Tkey>(string query, params Tkey[] parameters);

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete<Tkey>(Tkey id);

        void Delete(TEntity entity);

        IQueryable<TEntity> Queryable();

    }
}
