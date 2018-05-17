using System;
using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели серии с моделью хранения данных серии
    /// </summary>
    public class EpisodeMappingProfile : Profile
    {
        public EpisodeMappingProfile()
        {
            CreateMap<Episode, EpisodeEntity>()
                .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => src.Duration.TotalMinutes))
                .ReverseMap()
                .ForMember(dest => dest.Duration, opt => opt.ResolveUsing(src => TimeSpan.FromMinutes(src.DurationInMinutes)));
        }
    }
}