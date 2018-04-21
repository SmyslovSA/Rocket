using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models
{
    public class SimpleUser:User
    {
        public string Avatar { get; set; }
        public ICollection<string> Email { get; set; }
        public IDictionary<IEnumerable<string>,ICollection<string>> Categories { get; set; }
    }
}
