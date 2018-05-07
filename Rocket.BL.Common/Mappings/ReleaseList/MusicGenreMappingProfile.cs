using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели музыкальных жанров с моделью хранения данных музыкальных жанров
    /// </summary>
    public class MusicGenreMappingProfile : Profile
	{
		public MusicGenreMappingProfile()
		{
			CreateMap<MusicGenre, DbMusicGenre>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ReverseMap();
		}
	}
}
