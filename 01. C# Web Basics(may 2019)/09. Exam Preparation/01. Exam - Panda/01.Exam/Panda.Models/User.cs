using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Packages = new List<Package>();
            this.Receipts = new List<Receipt>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual List<Package> Packages { get; set; }

        public virtual List<Receipt> Receipts { get; set; }
    }
}
