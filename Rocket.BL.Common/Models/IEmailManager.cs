using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models
{
    interface IEmailManager
    {
        bool AddEmail(SimpleUser user, string email);
        bool DeleteEmail(SimpleUser user, string email);
    }
}
