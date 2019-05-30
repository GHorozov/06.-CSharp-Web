using System;
using System.Collections.Generic;

namespace IRunes.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price { get; set; }

        public List<Track> Tracks { get; set; } = new List<Track>();
    }
}
