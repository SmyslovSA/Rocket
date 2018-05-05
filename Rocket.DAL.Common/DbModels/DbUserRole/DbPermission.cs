using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbUserRole
{
    public class DbPermission
    {
        // класс для управления доавлением пермишенов.

        /// <summary>
        /// Уникальный идентификатор значения "право доступа" 
        /// (либо функциональная возможность)
        /// </summary>
        public ushort Id { get; set; }

        /// <summary>
        /// Описание  функц. возможности, соответствующее идентификатору 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Именование переменной, за которой скрывается реализация фичи
        /// </summary>
        public string ValueName { get; set; }
    }
}