using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models
{
    public class SimpleUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICollection<string> Email { get; set; }
        public IDictionary<IEnumerable<string>,ICollection<string>> Categories { get; set; }
    }
}
