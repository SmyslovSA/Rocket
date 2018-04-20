using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Models.ReleaseList
{
    public class TVSeries
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public ICollection<string> Countries { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<Season> Seasons { get; set; }

        public string PosterImagePath { get; set; }


    }
}
