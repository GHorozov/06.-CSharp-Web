namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [RegularExpression(@"[^A-Z]+?|[^a-z]+?|[^0-9]+?|[^!@#$%^&*()_+<>?]+?")]
        public string Password { get; set; }


        [Required]
        [RegularExpression(@"^[^_\-.][A-Za-z0-9_\-.]+[^_\-.]@[^_\-.][a-z]+.[a-z]+[^_\-.]$")]
        public string Email { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime  RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1,120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public List<UserFriend> Friends { get; set; } = new List<UserFriend>();
        public List<Album> Albums { get; set; } = new List<Album>();
        public List<UserAlbum> UserAlbum { get; set; } = new List<UserAlbum>();
    }
}
