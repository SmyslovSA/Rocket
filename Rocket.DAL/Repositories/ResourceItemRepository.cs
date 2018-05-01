using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Rocket.DAL.Common.Context;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;

namespace Rocket.DAL.Repositories
{
    public class ResourceItemRepository : IResourceItemRepository
    {
        private readonly IRocketContext _rocketContext;

        public ResourceItemRepository(IRocketContext rocketContext)
        {
            _rocketContext = rocketContext;
        }

        public void Delete(object id)
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

        public ResourceItemEntity GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(ResourceItemEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ResourceItemEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
