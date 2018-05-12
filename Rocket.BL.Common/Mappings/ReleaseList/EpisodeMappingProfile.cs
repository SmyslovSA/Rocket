using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели серии с моделью хранения данных серии
    /// </summary>
    public class EpisodeMappingProfile : Profile
    {
        public EpisodeMappingProfile()
        {
            CreateMap<Episode, DbEpisode>()
                .IncludeBase<BaseRelease, DbBaseRelease>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ReverseMap();
        }
    }
}
