using System.Collections.Generic;
using Rocket.BL.Common.Enums;

namespace Rocket.BL.Common.Models
{
    //персональный список ожидания релизов по категориям  
    public class PersonalizedTape
    {
        /// <summary>
        /// список ожидания релизов по категориям, представленный в виде "категория - список жанров"
        /// </summary>
        public IDictionary<IEnumerable<Categories>, ICollection<Genres>> PersonalTape { get; set; }       
    }
}
