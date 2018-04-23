using System.Collections.Generic;
using Rocket.BL.Common.Enums;

namespace Rocket.BL.Common.Models
{
    public class PersonalizedTape
    {
        /// <summary>
        /// персональный список релизов в виде: категория продукта - список жанров продукта
        /// </summary>
        public IDictionary<Categories, ICollection<Genres>> Categories { get; set; }    
    }
}
