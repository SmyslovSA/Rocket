﻿using System.Linq;
using AutoMapper;
using Rocket.BL.Common.Models.Notification;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.DbModels.User;

namespace Rocket.BL.Common.Mappings.Notification
{
    public class MusicNotificationMappingProfile : Profile
    {
        public MusicNotificationMappingProfile()
        {
            CreateMap<DbUser, Receiver>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Emails, opt => opt.MapFrom(s => s.DbAuthorisedUser.Email.Select(x => x.Name).ToList()));

            CreateMap<DbMusic, MusicNotification>()
                .ForMember(d => d.Receivers, opt => opt.MapFrom(s => s.Users))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.ReleaseDate, opt => opt.MapFrom(s => s.ReleaseDate))
                .ForMember(d => d.Musicians, opt => opt.MapFrom(s => s.Musicians));
        }
    }
}