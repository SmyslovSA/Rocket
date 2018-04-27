using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Тип адреса. Классика.
    /// </summary>
    class Address : IAddress
    {
        public string Index { get; set; }

        public Country Country { get; set; }

        public string Region { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string Building { get; set; }

        // Корпус.
        public string BuildingBlock { get; set; }

        public string Flat { get; set; }
    }
}
