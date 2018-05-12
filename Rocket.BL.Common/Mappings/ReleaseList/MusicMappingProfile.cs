using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели музыкального релиза с моделью хранения данных музыки
    /// </summary>
    public class MusicMappingProfile : Profile
	{
		public MusicMappingProfile()
		{
			CreateMap<Music, DbMusic>()
                .IncludeBase<BaseRelease, DbBaseRelease>()
                .ForMember(dest => dest.PosterImagePath, opt => opt.MapFrom(src => src.PosterImagePath))
				.ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
				.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
				.ForMember(dest => dest.MusicTracks, opt => opt.MapFrom(src => src.MusicTracks))
				.ForMember(dest => dest.Musicians, opt => opt.MapFrom(src => src.Musicians))
				.ReverseMap();
		}
	}
}
