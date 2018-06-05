using System.Linq;
using AutoMapper;
using Rocket.BL.Common.Models.Notification;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.BL.Common.Mappings.Notification
{
    public class TvSeriesNotificationMappingProfile : Profile
    {
        public TvSeriesNotificationMappingProfile()
        {
            CreateMap<DbUser, Receiver>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s =>
                    s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s =>
                    s.LastName))
                .ForMember(d => d.Emails, opt => opt.MapFrom(s =>
                    s.DbAuthorisedUser.Email.Select(x => x.Name).ToList()));

            CreateMap<TvSeriasEntity, TvSeriesNotification>()
                .ForMember(d => d.Receivers, opt => opt.MapFrom(s =>
                    s.Users))
                .ForMember(d => d.Title, opt => opt.MapFrom(s =>
                    s.TitleRu))
                .ForMember(d => d.SeasonNumber, opt => opt.MapFrom(s =>
                    s.ListSeasons.Last().Number))
                .ForMember(d => d.EpisodeNumber, opt => opt.MapFrom(s =>
                    s.ListSeasons.Last().ListEpisode.Last().Number))
                .ForMember(d => d.ReleaseDate, opt => opt.MapFrom(s =>
                    s.ListSeasons.Last().ListEpisode.Last().ReleaseDateRu));
        }
    }
}