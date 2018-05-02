using AutoMapper;
using Rocket.BL.Common.Models.User;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings.UserMappingProfile.UserPersonMappingProfile
{
    /// <summary>
    /// Профиль сопоставления доменной модели локализации пользователя с моделью хранения локализации пользователя
    /// </summary>
    public class LocalizationMappingProfile : Profile
    {
        public LocalizationMappingProfile()
        {
            CreateMap<Localization, DbLocalization>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Sitizenship, opt => opt.MapFrom(src => src.Sitizenship))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ReverseMap();
        }
    }
}
