using AutoMapper;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;

namespace Rocket.BL.Common.Mappings.PersonalAreaMappings
{
    /// <summary>
    /// Настройка маппинга между доменной моделью SimpleUser и моделью хранения DbAuthorisedUser.
    /// </summary>
    public class AuthorisedUserMappingProfile : Profile
    {
        public AuthorisedUserMappingProfile()
        {
            CreateMap<SimpleUser, DbAuthorisedUser>()
                .ForMember(dbu => dbu.DbUserId, u => u.MapFrom(src => src.Id))
                .ForPath(dbu => dbu.DbUser.FirstName, u => u.MapFrom(src => src.FirstName))
                .ForPath(dbu => dbu.DbUser.LastName, u => u.MapFrom(src => src.LastName))
                //.ForPath(dbu => dbu.DbUser.Login, u => u.MapFrom(src => src.Login))
                .ForMember(dbu => dbu.Avatar, u => u.MapFrom(src => src.Avatar))
                .ForMember(dbu => dbu.Email, u => u.MapFrom(src => src.Emails))
                .ForMember(dbu => dbu.Genres, u => u.MapFrom(src => src.Genres))
                .ForMember(dbu => dbu.MusicGenres, u => u.MapFrom(src => src.MusicGenre))
                .ReverseMap();
        }
    }
}