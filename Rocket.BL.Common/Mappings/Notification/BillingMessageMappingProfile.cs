using AutoMapper;
using Rocket.BL.Common.Models.Notification;
using Rocket.DAL.Common.DbModels.DbPersonalArea;

namespace Rocket.BL.Common.Mappings.Notification
{
    class BillingMessageMappingProfile : Profile
    {
        public BillingMessageMappingProfile()
        {
            CreateMap<DbAuthorisedUser, BillingNotification>()
                .ForMember(d => d.Receiver.FirstName, opt => opt.MapFrom(s => s.
                        DbUser.FirstName))
                .ForMember(d => d.Receiver.LastName, opt => opt.MapFrom(s => s.
                        DbUser.LastName));
        }
    }
}