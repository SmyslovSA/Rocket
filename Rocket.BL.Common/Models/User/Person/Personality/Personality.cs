using System;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Некие субуго личностные, характеризующие данные,
    /// дата рождения, пол.
    /// </summary>
    public interface IPersonality
    {
        // Дата рождения.
        DateTime? DateOfBirth { get; set; }

        // Пол.
        SexType? Sex { get; set; }
    }
}
