using AutoMapper;
using Rocket.BL.Common.Models.User;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.BL.Common.Mappings.UserMappingProfile
{
    /// <summary>
    /// Профиль сопоставления доменной модели пользователя с моделью хранения данных пользователя
    /// </summary>
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, DbUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.AccountStatus))
                .ForMember(dest => dest.AccountLevel, opt => opt.MapFrom(src => src.AccountLevel))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
                .ForMember(dest => dest.ActivationNeeded, opt => opt.MapFrom(src => src.ActivationNeeded))
                .ForMember(dest => dest.Sitizenship, opt => opt.MapFrom(src => src.Sitizenship))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.HowToCall, opt => opt.MapFrom(src => src.HowToCall))
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones))
                .ForMember(dest => dest.EMailAddresses, opt => opt.MapFrom(src => src.EMailAddresses))
                .ForMember(dest => dest.MailAddress, opt => opt.MapFrom(src => src.MailAddress))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
                .ReverseMap();
        }
    }
}
