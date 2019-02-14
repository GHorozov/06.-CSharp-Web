namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;
    using SocialNetwork.Models.Attributes;
    using System.Collections.Generic;

    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [TagAttribute]
        public string Name { get; set; }

        public List<AlbumTag> Albums { get; set; } = new List<AlbumTag>();
    }
}
