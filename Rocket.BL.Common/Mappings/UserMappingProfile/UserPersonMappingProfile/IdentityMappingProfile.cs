using AutoMapper;
using Rocket.BL.Common.Models.User.Person.Identity;
using Rocket.DAL.Common.DbModels.DbUser.DbPerson;

namespace Rocket.BL.Common.Mappings.UserMappingProfile.UserPersonMappingProfile
{
    /// <summary>
    /// Профиль сопоставления доменной модели сведений, неотъемлемые от личности
    /// с моделью хранения данных о сведениях, неотъемлемых от личности
    /// </summary>
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<Identity, DbIdentity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ReverseMap();
        }
    }
}
