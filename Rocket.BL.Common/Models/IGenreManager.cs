using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models
{
    interface IGenreManager
    {
        bool AddGenre(SimpleUser user, IEnumerable<string> categories, IEnumerable<string> genre);
        bool DeleteGenre(SimpleUser user, IEnumerable<string> categories, IEnumerable<string> genre);
    }
}
