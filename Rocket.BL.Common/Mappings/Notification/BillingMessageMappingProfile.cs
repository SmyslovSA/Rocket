using AutoMapper;
using Rocket.BL.Common.Models.Notification;
using Rocket.DAL.Common.DbModels.DbPersonalArea;

namespace Rocket.BL.Common.Mappings.Notification
{
    public class BillingMessageMappingProfile : Profile
    {
        public BillingMessageMappingProfile()
        {
            CreateMap<DbAuthorisedUser, BillingNotification>()
                .ForPath(d => d.Receiver.FirstName, opt => opt.MapFrom(s => s.DbUser.FirstName))
                .ForPath(d => d.Receiver.LastName, opt => opt.MapFrom(s => s.DbUser.LastName));
        }
    }
}