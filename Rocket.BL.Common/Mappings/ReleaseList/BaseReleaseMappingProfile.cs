using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.BL.Common.Mappings.ReleaseList
{
    /// <summary>
    /// Профиль сопоставления доменной модели релиза с моделью хранения данных релиза
    /// </summary>
    public class BaseReleaseMappingProfile : Profile
    {
        public BaseReleaseMappingProfile()
        {
            CreateMap<BaseRelease, DbBaseRelease>()
                .Include<Film, DbFilm>()
                .Include<Episode, DbEpisode>()
                .Include<Music, DbMusic>()
                .ReverseMap();
        }
    }
}
