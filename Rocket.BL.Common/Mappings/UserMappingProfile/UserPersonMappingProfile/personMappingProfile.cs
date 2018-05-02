using AutoMapper;
using Rocket.BL.Common.Models.User;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings
{
    /// <summary>
    /// Профиль сопоставления доменной модели человека (личности) пользователя с моделью хранения личности (человека) пользователя
    /// </summary>
    public class personMappingProfile : Profile
    {
        public personMappingProfile()
        {
            CreateMap<Person, Dbperson>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Personality, opt => opt.MapFrom(src => src.Personality))
                .ForMember(dest => dest.Communication, opt => opt.MapFrom(src => src.Communication))
                .ForMember(dest => dest.Locallization, opt => opt.MapFrom(src => src.Locallization))
                .ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.Identity))
                .ReverseMap();
        }
    }
}
