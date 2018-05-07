using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели сезона с моделью хранения данных сезона
    /// </summary>
    public class SeasonMappingProfile : Profile
    {
        public SeasonMappingProfile()
        {
            CreateMap<Season, DbSeason>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.PosterImagePath, opt => opt.MapFrom(src => src.PosterImagePath))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes))
                .ReverseMap();
        }
    }
}
