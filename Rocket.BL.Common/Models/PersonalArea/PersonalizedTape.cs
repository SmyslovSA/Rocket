using System.Collections.Generic;

namespace Rocket.BL.Common.Models
{
    /// <summary>
    /// класс, содержащий данные о персональном списке релизов User
    /// </summary>
    public class PersonalizedTape
    {
        /// <summary>
        /// Id списка релизов
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// персональный список релизов в виде списка жанров 
        /// </summary>
        public ICollection<Genre> Genres { get; set; }    
    }
}
