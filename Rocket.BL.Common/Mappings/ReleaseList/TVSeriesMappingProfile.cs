using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Models.Subscription;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.Subscription;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели сериала с моделью хранения данных сериала
    /// </summary>
    public class TVSeriesMappingProfile : Profile
    {
        public TVSeriesMappingProfile()
        {
            CreateMap<TVSeries, TvSeriasEntity>()
                .IncludeBase<Subscribable, SubscribableEntity>()
                .ForMember(dest => dest.ListGenreEntity, opt => opt.MapFrom(src => src.Genres))
                .ReverseMap();
        }
    }
}