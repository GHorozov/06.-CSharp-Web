namespace MyCoolWebServer.ByTheCakeApplication.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
        }

        public Product(string name, decimal price, string imageUrl)
        {
            this.Name = name;
            this.Price = price;
            this.ImageUrl = imageUrl;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public List<OrderProduct> Orders { get; set; } = new List<OrderProduct>();
    }
}