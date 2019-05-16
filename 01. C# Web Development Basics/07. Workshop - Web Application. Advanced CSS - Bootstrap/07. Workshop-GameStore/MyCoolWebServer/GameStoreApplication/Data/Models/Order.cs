namespace MyCoolWebServer.GameStoreApplication.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<OrderGame> Games { get; set; } = new List<OrderGame>();
    }
}
