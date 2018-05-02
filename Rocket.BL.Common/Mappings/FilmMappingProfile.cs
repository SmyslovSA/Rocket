using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings
{
    /// <summary>
    /// Профиль сопоставления доменной модели фильма с моделью хранения данных фильма
    /// </summary>
    public class FilmMappingProfile : Profile
    {
        public FilmMappingProfile()
        {
            CreateMap<Film, DbFilm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PosterImagePath, opt => opt.MapFrom(src => src.PosterImagePath))
                .ForMember(dest => dest.Directors, opt => opt.MapFrom(src => src.Directors))
                .ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.Cast))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.Countries))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.TrailerLink, opt => opt.MapFrom(src => src.TrailerLink))
                .ReverseMap();
        }
    }
}
