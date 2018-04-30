using Rocket.DAL.Common.Context;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.UoW;

namespace Rocket.DAL.UoW
{
    public class ParserUoW : IParserUoW
    {
        private readonly IRocketContext _rocketContext;

        public ParserUoW(IRocketContext rocketContext, IResourceEntityRepository resourceEntityRepository)
        {
            _rocketContext = rocketContext;
            ResourceEntities = resourceEntityRepository;
        }   

        public IResourceEntityRepository ResourceEntities { get; }
        
        public void Save()
        {
            _rocketContext.SaveChanges();
        }

        public void SaveAsync()
        {
            _rocketContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _rocketContext.Dispose();
        }

    }
}
