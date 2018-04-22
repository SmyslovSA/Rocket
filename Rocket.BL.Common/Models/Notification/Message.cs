using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.BL.Common.Models.Notification;

namespace Rocket.BL.Common.Models
{
    /// <summary>
    /// Describes message
    /// </summary>
    class Message
    {
        /// <summary>
        /// Gets or sets the reciever of
        /// the current message
        /// </summary>
        public Reciever Reciever { get; set; }

        /// <summary>
        /// Gets or sets the sender of
        /// the current message
        /// </summary>
        public Sender Sender { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// of the message
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body of the
        /// message
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets settings of the current
        /// transfer session
        /// </summary>
        public ConnectionSettings Settings { get; set; }
    }
}