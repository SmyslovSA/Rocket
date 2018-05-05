using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Context;

namespace Rocket.DAL.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly RocketContext _rocketContext;

        public ResourceRepository(RocketContext rocketContext)
        {
            _rocketContext = rocketContext;
        }

        public void Delete<T>(T id)
        {
            _rocketContext.Resources.Remove(_rocketContext.Resources.Find(id) 
                ?? throw new InvalidOperationException());
        }

        public void Delete(ResourceEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResourceEntity> Get(Expression<Func<ResourceEntity, bool>> filter = null, Func<IQueryable<ResourceEntity>, IOrderedQueryable<ResourceEntity>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public ResourceEntity GetById<T>(T id)
        {
            return _rocketContext.Resources.Find(id) ?? throw new InvalidOperationException();
        }

        public void Insert(ResourceEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ResourceEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
