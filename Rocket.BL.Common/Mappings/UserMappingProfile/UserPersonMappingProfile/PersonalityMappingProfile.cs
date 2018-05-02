using AutoMapper;
using Rocket.BL.Common.Models.User;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings
{
    /// <summary>
    /// Профиль сопоставления доменной модели идентификатора личности пользователя с моделью хранения данных идентификатора личности
    /// </summary>
    public class PersonalityMappingProfile : Profile
    {
        public PersonalityMappingProfile()
        {
            CreateMap<Personality, DbPersonality>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ReverseMap();
        }
    }
}
