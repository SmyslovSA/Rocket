using System;
using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Тип адреса. Классика.
    /// </summary>
    public interface IAddress
    {
        string Index { get; set; }
        
        Country Country { get; set; }

        string Region { get; set; }

        string District { get; set; }
        
        string City { get; set; }

        string Building { get; set; }

        // Корпус.
        string BuildingBlock { get; set; }
        
        string Flat { get; set; }
    }
}
