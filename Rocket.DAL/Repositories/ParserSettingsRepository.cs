using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Rocket.DAL.Common.Context;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;

namespace Rocket.DAL.Repositories
{
    public class ParserSettingsRepository : IParserSettingsRepository
    {
        private readonly IRocketContext _rocketContext;

        public ParserSettingsRepository(IRocketContext rocketContext)
        {
            _rocketContext = rocketContext;
        }

        public void Delete<T>(T id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ParserSettingsEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParserSettingsEntity> Get(Expression<Func<ParserSettingsEntity, bool>> filter = null, Func<IQueryable<ParserSettingsEntity>, IOrderedQueryable<ParserSettingsEntity>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public ParserSettingsEntity GetById<T>(T id)
        {
            return _rocketContext.ParserSettings.Find(id) 
                ?? throw new InvalidOperationException();  // todo customize ex
        }

        public void Insert(ParserSettingsEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ParserSettingsEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
