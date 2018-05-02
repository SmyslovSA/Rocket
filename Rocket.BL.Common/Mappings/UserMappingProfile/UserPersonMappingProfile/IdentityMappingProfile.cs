using AutoMapper;
using Rocket.BL.Common.Models.User;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings
{
    /// <summary>
    /// Профиль сопоставления доменной модели сведений, неотъемлемые от личности
    /// с моделью хранения данных о сведениях, неотъемлемые от личности
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
