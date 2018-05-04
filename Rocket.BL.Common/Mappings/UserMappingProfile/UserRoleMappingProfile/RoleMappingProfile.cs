using AutoMapper;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.BL.Common.Mappings.UserMappingProfile.UserRoleMappingProfile
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, DbRole>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions))
                .ReverseMap();
        }
    }
}