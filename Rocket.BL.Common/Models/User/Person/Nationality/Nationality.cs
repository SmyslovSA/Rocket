using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Собрана вся информация, касающаяся
    /// географических аспектов пользователя.
    /// Его гражданство, язык.
    /// </summary>
    public class Nationality
    {
        // Гражданство.
        Country Sitizenship { get; set; }
        
        // Язык.
        Country Language { get; set; }
    }
}
