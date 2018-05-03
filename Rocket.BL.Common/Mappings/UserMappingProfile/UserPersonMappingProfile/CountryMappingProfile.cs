using AutoMapper;
using Rocket.BL.Common.Models.User.Person.Localization;
using Rocket.DAL.Common.DbModels.DbUser.DbPerson;

namespace Rocket.BL.Common.Mappings.UserMappingProfile.UserPersonMappingProfile
{
    /// <summary>
    /// Профиль сопоставления доменной модели страны для адреса пользователя с моделью хранения данных страны для адреса пользователя
    /// </summary>
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, DbCountry>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
