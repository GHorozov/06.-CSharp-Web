namespace MyCoolWebServer.GameStoreApplication.ViewModels.Cart
{
    using System;

    public class CartGameViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string VideoId { get; set; }

        public string ImageUrl { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
