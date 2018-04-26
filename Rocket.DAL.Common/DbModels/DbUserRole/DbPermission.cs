﻿using System.Collections.Generic;

namespace Rocket.DAL.Common.DbModels.DbUserRole
{
    public class DbPermission
    {
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
        /// Задает или получает список ролей, у которых
        /// есть текущая функциональная возможность
        /// </summary>
        public ICollection<DbRole> Roles { get; set; }
    }
}