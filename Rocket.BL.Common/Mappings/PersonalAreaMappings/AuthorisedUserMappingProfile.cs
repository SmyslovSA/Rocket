using AutoMapper;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;

namespace Rocket.BL.Common.Mappings.PersonalAreaMappings
{
    /// <summary>
    /// настройка маппинга между доменной моделью User и моделью хранения DbAuthorisedUser
    /// </summary>
    public class AuthorisedUserMappingProfile : Profile
    {
        public AuthorisedUserMappingProfile()
        {
            CreateMap<SimpleUser, DbAuthorisedUser>().
                ForMember(dbu => dbu.Id, u => u.MapFrom(src => src.Id)).
                ForMember(dbu => dbu.DbPersonality.FirstName, u => u.MapFrom(src => src.FirstName)).
                ForMember(dbu => dbu.DbPersonality.LastName, u => u.MapFrom(src => src.LastName)).
                ForMember(dbu => dbu.DbAccount.Login, u => u.MapFrom(src => src.Login)).
                ForMember(dbu => dbu.Avatar, u => u.MapFrom(src => src.Avatar)).
                ForMember(dbu => dbu.Email, u => u.MapFrom(src => src.Email)).
                ForMember(dbu => dbu.Genres, u => u.MapFrom(src => src.Personalized.Genres)).
                ReverseMap();
        }
    }
}
