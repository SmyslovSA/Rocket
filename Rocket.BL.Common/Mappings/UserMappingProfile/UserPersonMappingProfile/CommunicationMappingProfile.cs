using AutoMapper;
using Rocket.BL.Common.Models.User.Person.Communication;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings.UserMappingProfile.UserPersonMappingProfile
{
    /// <summary>
    /// Профиль сопоставления доменной модели контактных данных пользователя с моделью хранения контактных данных
    /// </summary>
    public class CommunicationMappingProfile : Profile
    {
        public CommunicationMappingProfile()
        {
            CreateMap<Communication, DbCommunication>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.HowToCall, opt => opt.MapFrom(src => src.HowToCall))
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones))
                .ForMember(dest => dest.EMailAddresses, opt => opt.MapFrom(src => src.EMailAddresses))
                .ForMember(dest => dest.MailAddress, opt => opt.MapFrom(src => src.MailAddress))
                .ReverseMap();
        }
    }
}
