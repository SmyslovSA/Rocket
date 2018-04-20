using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models
{
    class EmailMessage
    {
        public string ReceiverEmailAddress { get; set; }

        public string SenderEmailAddress { get; set; }

        public string SenderEmailPassword { get; set; }
        
        public string MessageBody { get; set; }

        public string MessageTheme { get; set; }
    }
}
