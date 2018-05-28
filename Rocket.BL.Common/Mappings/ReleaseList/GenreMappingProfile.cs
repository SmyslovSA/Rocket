using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Models.Subscription;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.Subscription;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<Genre, GenreEntity>()
                .IncludeBase<Subscribable, SubscribableEntity>()
                .ReverseMap();
        }
    }
}
