namespace MyCoolWebServer.ByTheCakeApplication.Data.Models
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

        public List<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    }
}
