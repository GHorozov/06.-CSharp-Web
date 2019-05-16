namespace MyCoolWebServer.GameStoreApplication.ViewModels.Game
{
    using System;

    public class GameDetailsViewModel
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
