using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Models
{
    public class Track
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public decimal Price { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
