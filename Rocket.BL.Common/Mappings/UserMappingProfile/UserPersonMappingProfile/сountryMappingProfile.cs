using AutoMapper;
using Rocket.BL.Common.Models.User;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings
{
    /// <summary>
    /// Профиль сопоставления доменной модели страны для адреса пользователя с моделью хранения данных страны для адреса пользователя
    /// </summary>
    public class сountryMappingProfile : Profile
    {
        public сountryMappingProfile()
        {
            CreateMap<Country, Dbcountry>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
