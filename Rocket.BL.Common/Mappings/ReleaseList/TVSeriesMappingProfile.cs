using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.Parser;

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
                .ForMember(dest => dest.ListGenreEntity, opt => opt.MapFrom(src => src.Genres))
                .ReverseMap();
        }
    }
}