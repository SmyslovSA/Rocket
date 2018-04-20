using System;
using System.Collections.Generic;

namespace Rocket.BL.Common.Models.ReleaseList
{
    public class Film
    {
        public int Id { get; set; }

        public Release Release { get; set; }

        public string Title { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public TimeSpan Duration { get; set; }

        public string Summary { get; set; }

        public ICollection<string> Countries { get; set; }

        public string PosterImagePath { get; set; }

        public ICollection<Person> Directors { get; set; }

        public ICollection<Person> Cast { get; set; }

        public string TrailerLink { get; set; }

    }
}