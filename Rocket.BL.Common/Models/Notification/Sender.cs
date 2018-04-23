using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Describes administrator or system
    /// which sends the message
    /// </summary>
    class Sender
    {
        /// <summary>
        /// Gets or sets the administrator
        /// or system name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the administrator
        /// or system email address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password of the
        /// administrator or system account
        /// </summary>
        public string Password { get; set; }
    }
}