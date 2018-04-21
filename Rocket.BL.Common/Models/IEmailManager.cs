﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models
{
    interface IEmailManager
    {
        bool AddEmail(User user, string email);
        bool DeleteEmail(User user, string email);
    }
}
