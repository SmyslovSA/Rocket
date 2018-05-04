using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели жанра видео с моделью хранения данных жанра видео
    /// </summary>
    public class VideoGenreMappingProfile : Profile
    {
        public VideoGenreMappingProfile()
        {
            CreateMap<VideoGenre, DbVideoGenre>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
