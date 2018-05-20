using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<Genre, GenreEntity>().ReverseMap();
        }
    }
}
