using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models.UserRoles
{
    public interface IRole
    {
        /// <summary>
        /// Уникальный идентификатор роли
        /// </summary>
        ushort RoleId { get; set; }
    }
}
