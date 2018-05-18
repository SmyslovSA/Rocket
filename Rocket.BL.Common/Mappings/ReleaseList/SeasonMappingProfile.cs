using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели сезона с моделью хранения данных сезона
    /// </summary>
    public class SeasonMappingProfile : Profile
    {
        public SeasonMappingProfile()
        {
            CreateMap<Season, SeasonEntity>().ReverseMap();
        }
    }
}