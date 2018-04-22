using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Describes a properties of
    /// the current transfer session
    /// </summary>
    class ConnectionSettings
    {
        /// <summary>
        /// Gets or sets the host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the server port
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// Gets or sets the state of
        /// SSL encryption
        /// </summary>
        public bool Ssl { get; set; }

        /// <summary>
        /// Gets or sets the state of
        /// TLS encryption
        /// </summary>
        public bool Tls { get; set; }
    }
}