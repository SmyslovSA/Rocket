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
        /// персональный список релизов в виде: категория продукта - список жанров продукта
        /// </summary>
        public IDictionary<Category, ICollection<Genre>> Categories { get; set; }    
    }
}
