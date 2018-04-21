using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models
{
    interface IGenreManager
    {
        bool AddGenre(User user, IEnumerable<string> categories, IEnumerable<string> genre);
        bool DeleteGenre(User user, IEnumerable<string> categories, IEnumerable<string> genre);
    }
}
