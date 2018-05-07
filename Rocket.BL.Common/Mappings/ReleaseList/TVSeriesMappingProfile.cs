using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели сериала с моделью хранения данных сериала
    /// </summary>
    public class TVSeriesMappingProfile : Profile
    {
        public TVSeriesMappingProfile()
        {
            CreateMap<TVSeries, DbTVSeries>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PosterImagePath, opt => opt.MapFrom(src => src.PosterImagePath))
                .ForMember(dest => dest.Directors, opt => opt.MapFrom(src => src.Directors))
                .ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.Cast))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.Countries))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.DbSeasons, opt => opt.MapFrom(src => src.Seasons))
                .ReverseMap();
        }
    }
}
