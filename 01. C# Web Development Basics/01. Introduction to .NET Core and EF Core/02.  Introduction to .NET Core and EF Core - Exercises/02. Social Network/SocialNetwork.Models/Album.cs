using System.Collections.Generic;

namespace SocialNetwork.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Picture> Pictures { get; set; } = new List<Picture>();
        public List<AlbumTag> Tags { get; set; } = new List<AlbumTag>();
        public List<UserAlbum> UserAlbum { get; set; } = new List<UserAlbum>();
    }
}
