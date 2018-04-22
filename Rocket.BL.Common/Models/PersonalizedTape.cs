using System.Collections.Generic;
using Rocket.BL.Common.Enums;

namespace Rocket.BL.Common.Models
{
    public class PersonalizedTape
    {
        public IDictionary<IEnumerable<Categories>, ICollection<Genres>> PersonalTape { get; set; }       
    }
}
