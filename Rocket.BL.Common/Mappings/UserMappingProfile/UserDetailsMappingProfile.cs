using AutoMapper;
using Rocket.BL.Common.Models.User;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.BL.Common.Mappings.UserMappingProfile
{
    /// <summary>
    /// Профиль сопоставления доменной модели детальной информации пользователя с моделью хранения данных детальной информации пользователя
    /// </summary>
    public class UserDetailsMappingProfile : Profile
    {
        public UserDetailsMappingProfile()
        {
            CreateMap<UserDetails, DbUserDetails>()
                .ForMember(dest => dest.ActivationNeeded, opt => opt.MapFrom(src => src.ActivationNeeded))
                .ForMember(dest => dest.Sitizenship, opt => opt.MapFrom(src => src.Sitizenship))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.HowToCall, opt => opt.MapFrom(src => src.HowToCall))
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones))
                .ForMember(dest => dest.EMailAddresses, opt => opt.MapFrom(src => src.EMailAddresses))
                .ForMember(dest => dest.MailAddress, opt => opt.MapFrom(src => src.MailAddress))
                .ReverseMap();
        }
    }
}