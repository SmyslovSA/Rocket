using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models
{
    interface IPersonalData
    {
        bool ChangePersonalData(SimpleUser user, string firstName, string lastName, string avatar);
        bool ChangePasswordData(SimpleUser user, string newPassword, string newPasswordConfirm);
    }
}
