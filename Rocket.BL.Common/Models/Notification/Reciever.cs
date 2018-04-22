using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Describes user or guest which
    /// is the reciever of message
    /// </summary>
    class Reciever
    {
        /// <summary>
        /// Gets or sets the current user or guest name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the current user or guest
        /// email address
        /// </summary>
        public string EmailAddress { get; set; }
    }
}