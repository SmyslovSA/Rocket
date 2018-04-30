using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Rocket.DAL.Common.Context;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Context;

namespace Rocket.DAL.Repositories
{
    public class ResourceEntityRepository : IResourceEntityRepository
    {
        private readonly IRocketContext _rocketContext;

        public ResourceEntityRepository(IRocketContext rocketContext)
        {
            _rocketContext = rocketContext;
        }

        public void Delete(object id)
        {
            _rocketContext.Resources.Remove(_rocketContext.Resources.Find((int) id) 
                ?? throw new InvalidOperationException());

            //var resourceEntity = _rocketContext.Resources.Find((int) id);
            //_rocketContext.Resources.Attach(resourceEntity);
            //_rocketContext.Entry(resourceEntity).State = EntityState.Deleted;
        }

        public void Delete(ResourceEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResourceEntity> Get(Expression<Func<ResourceEntity, bool>> filter = null, Func<IQueryable<ResourceEntity>, IOrderedQueryable<ResourceEntity>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public ResourceEntity GetById(object id)
        {
            return _rocketContext.Resources.Find((int)id) ?? throw new InvalidOperationException();
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
