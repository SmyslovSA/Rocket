using AutoMapper;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings.UserPayment
{
    /// <summary>
    /// Профиль сопоставления доменной модели платежа с моделью хранения данных о платеже.
    /// </summary>
    public class UserPaymentMappingProfile : Profile
    {
        public UserPaymentMappingProfile()
        {
            CreateMap<Models.UserPayment.UserPayment, DbUserPayment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Currentcy, opt => opt.MapFrom(src => src.Currentcy))
                .ForMember(dest => dest.CustomString, opt => opt.MapFrom(src => src.CustomString))
                .ForMember(dest => dest.PaymentID, opt => opt.MapFrom(src => src.PaymentID))
                .ForMember(dest => dest.Summ, opt => opt.MapFrom(src => src.Summ))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
                .ReverseMap();
        }
    }
}