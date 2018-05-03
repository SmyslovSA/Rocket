using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Rocket.DAL.Common.Context;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.UoW;

namespace Rocket.DAL.UoW
{
    public class ParserUoW : IParserUoW
    {
        private readonly IRocketContext _rocketContext;

        public ParserUoW(
            IRocketContext rocketContext, 
            IResourceRepository resourceRepository,
            IParserSettingsRepository parserSettingsRepository,
            IResourceItemRepository resourceItemRepository)
        {
            _rocketContext = rocketContext;
            Resources = resourceRepository;
            ParserSettings = parserSettingsRepository;
            ResourceItems = resourceItemRepository;
        }   

        public IResourceRepository Resources { get; }

        public IParserSettingsRepository ParserSettings { get; }

        public IResourceItemRepository ResourceItems { get; }

        public void Save()
        {
            _rocketContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _rocketContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _rocketContext.Dispose();
        }

        /// <summary>
        /// Возвращает список настроек парсера
        /// </summary>
        /// <param name="resourceName">Название ресурса для парсинга</param>
        /// <returns>Коллекция ParserSettingsEntity</returns>
        public ICollection<ParserSettingsEntity> GetParserSettingsByResourceName(string resourceName)
        {
            var resource = _rocketContext.Resources.Where(item => item.Name == resourceName).
                Include(r => r.ParserSettings).FirstOrDefault();

            if (resource != null && resource.ParserSettings.Any())
            {
                return resource.ParserSettings.ToList();
            }

            throw new NotImplementedException(); //todo

            //return null;
        }

    }
}
