using System.Collections.Generic;

namespace Rocket.BL.Common.Models
{
    public class Film
    {
        public int Id { get; set; }

        public Release Release { get; set; }

        public string Title { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}