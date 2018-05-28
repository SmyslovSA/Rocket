namespace Rocket.BL.Common.DtoModels.ReleaseList
{
    public class EpisodeFullDto : EpisodeDto
    {
        public double DurationInMinutes { get; set; }

        public string UrlForEpisodeSource { get; set; }
    }
}
