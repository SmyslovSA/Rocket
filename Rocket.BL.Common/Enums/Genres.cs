using System;

namespace Rocket.BL.Common.Enums
{
    [Flags]
    public enum Genres
    {
        Action = Categories.Movies | Categories.TVseries,
        Drama = Categories.Movies | Categories.TVseries,
        Rock = Categories.Music
    }
}
