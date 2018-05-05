using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Context;

namespace Rocket.DAL.Repositories
{
    public class ResourceItemRepository : IResourceItemRepository
    {
        private readonly RocketContext _rocketContext;

        public ResourceItemRepository(RocketContext rocketContext)
        {
            _rocketContext = rocketContext;
        }

        public void Delete<T>(T id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ResourceItemEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResourceItemEntity> Get(Expression<Func<ResourceItemEntity, bool>> filter = null, Func<IQueryable<ResourceItemEntity>, IOrderedQueryable<ResourceItemEntity>> orderBy = null, string includeProperties = "")
        {
            if (filter != null)
            {
                return _rocketContext.ResourceItems.Where(filter).ToList();
            }

            throw new NotImplementedException();
        }

        public ResourceItemEntity GetById<T>(T id)
        {
            throw new NotImplementedException();
        }

        public void Insert(ResourceItemEntity entity)
        {
            _rocketContext.ResourceItems.Add(entity);
        }

        public void Update(ResourceItemEntity entity)
        {
            var resourceItem = _rocketContext.ResourceItems.Find(entity.Id);
            if (resourceItem != null)
            {
                resourceItem = entity;
            }
            else
            {
                throw new NotImplementedException();  //todo
            }

            throw new NotImplementedException();
        }
    }
}
